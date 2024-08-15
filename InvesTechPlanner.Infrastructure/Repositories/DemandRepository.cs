using CsvHelper.Configuration;
using CsvHelper;
using Dapper;
using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;
using InvesTechPlanner.UseCases.Interfaces;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;

namespace InvesTechPlanner.Infrastructure.Repositories
{
    public class DemandRepository(IDatabaseConnectionFactory connectionFactory) : IDemandRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory = connectionFactory;

        public async Task Add(Demand demand)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string insertQuery = @"
                INSERT INTO Demand (Title, RequestedDept, RequestedBy, DemandOwner, DemandCategory, Objective, Proposal, InvestmentDetails, 
                                    BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, Status, InvestClassification, 
                                    InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, DateCreated, DateUpdated, 
                                    CreatedBy, IsInactive, Remarks)
                VALUES (@Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, @InvestmentDetails, 
                        @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, @Status, @InvestClassification, 
                        @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, @DateCreated, 
                        @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            demand.IsInactive = false; // IsInactiveを常にfalseに設定
            await connection.ExecuteAsync(insertQuery, demand);

            await InsertHistory(demand, connection);
        }

        public async Task<Demand?> GetById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM Demand WHERE DemandID = @Id";
            return await connection.QuerySingleOrDefaultAsync<Demand>(query, new { Id = id });
        }

        public async Task<IEnumerable<Demand>> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM Demand WHERE IsInactive = 0";
            return await connection.QueryAsync<Demand>(query);
        }

        public async Task<IEnumerable<Demand>> GetFilteredDemands(DemandFilterOptionsDto filterOptions)
        {
            using var connection = _connectionFactory.CreateConnection();

            var queryBuilder = new StringBuilder("SELECT * FROM Demand WHERE IsInactive = 0");

            if (!string.IsNullOrEmpty(filterOptions.FiscalYear))
            {
                queryBuilder.Append(" AND FiscalYear = @FiscalYear");
            }
            if (!string.IsNullOrEmpty(filterOptions.DemandCategory))
            {
                queryBuilder.Append(" AND DemandCategory = @DemandCategory");
            }
            if (!string.IsNullOrEmpty(filterOptions.RequestedDept))
            {
                queryBuilder.Append(" AND RequestedDept = @RequestedDept");
            }
            if (!string.IsNullOrEmpty(filterOptions.Status))
            {
                queryBuilder.Append(" AND Status = @Status");
            }
            if (!string.IsNullOrEmpty(filterOptions.InvestmentScale))
            {
                queryBuilder.Append(" AND InvestmentScale = @InvestmentScale");
            }
            if (!string.IsNullOrEmpty(filterOptions.DemandPriority))
            {
                queryBuilder.Append(" AND DemandPriority = @DemandPriority");
            }

            var query = queryBuilder.ToString();
            return await connection.QueryAsync<Demand>(query, filterOptions);
        }
    

    public async Task Update(Demand demand)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE Demand
                SET Title = @Title, RequestedDept = @RequestedDept, RequestedBy = @RequestedBy, DemandOwner = @DemandOwner, 
                    DemandCategory = @DemandCategory, Objective = @Objective, Proposal = @Proposal, InvestmentDetails = @InvestmentDetails, 
                    BusinessImpactIfNotConducted = @BusinessImpactIfNotConducted, RiskComment = @RiskComment, DemandPriority = @DemandPriority, 
                    Assumption = @Assumption, Dependency = @Dependency, Status = @Status, InvestClassification = @InvestClassification, 
                    InvestmentScale = @InvestmentScale, PMOResponsible = @PMOResponsible, ITBizPartner = @ITBizPartner, 
                    PlannedStart = @PlannedStart, PlannedEnd = @PlannedEnd, FiscalYear = @FiscalYear, DateUpdated = @DateUpdated, 
                    CreatedBy = @CreatedBy, IsInactive = @IsInactive, Remarks = @Remarks
                WHERE DemandID = @DemandID";

            await connection.ExecuteAsync(updateQuery, demand);
            await InsertHistory(demand, connection);
        }

        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE Demand
                SET IsInactive = 1
                WHERE DemandID = @Id";

            await connection.ExecuteAsync(updateQuery, new { Id = id });

            var demand = await GetById(id);
            if (demand != null)
            {
                await InsertHistory(demand, connection);
            }
        }

        private async Task InsertHistory(Demand demand, IDbConnection connection)
        {
            const string historyInsertQuery = @"
                INSERT INTO DemandHistory (DemandID, Title, RequestedDept, RequestedBy, DemandOwner, DemandCategory, Objective, Proposal, 
                                           InvestmentDetails, BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, 
                                           Status, InvestClassification, InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, 
                                           DateCreated, DateUpdated, CreatedBy, IsInactive, Remarks)
                VALUES (@DemandID, @Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, 
                        @InvestmentDetails, @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, 
                        @Status, @InvestClassification, @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, 
                        @DateCreated, @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(historyInsertQuery, demand);
        }

        public async Task<MemoryStream> ExportDemandsToCsv()
        {
            var demands = await GetAll();
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(demands);
            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
