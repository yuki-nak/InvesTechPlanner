using Dapper;
using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.Interfaces;


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
                                    BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, InvestClassification, 
                                    InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, DateCreated, DateUpdated, 
                                    CreatedBy, IsInactive, Remarks)
                VALUES (@Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, @InvestmentDetails, 
                        @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, @InvestClassification, 
                        @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, @DateCreated, 
                        @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(insertQuery, demand);

            // Historyテーブルへの挿入
            const string historyInsertQuery = @"
                INSERT INTO DemandHistory (DemandID, Title, RequestedDept, RequestedBy, DemandOwner, DemandCategory, Objective, Proposal, 
                                           InvestmentDetails, BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, 
                                           InvestClassification, InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, 
                                           DateCreated, DateUpdated, CreatedBy, IsInactive, Remarks)
                VALUES (@DemandID, @Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, 
                        @InvestmentDetails, @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, 
                        @InvestClassification, @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, 
                        @DateCreated, @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(historyInsertQuery, demand);
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
            const string query = "SELECT * FROM Demand";
            return await connection.QueryAsync<Demand>(query);
        }

        public async Task Update(Demand demand)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE Demand
                SET Title = @Title, RequestedDept = @RequestedDept, RequestedBy = @RequestedBy, DemandOwner = @DemandOwner, 
                    DemandCategory = @DemandCategory, Objective = @Objective, Proposal = @Proposal, InvestmentDetails = @InvestmentDetails, 
                    BusinessImpactIfNotConducted = @BusinessImpactIfNotConducted, RiskComment = @RiskComment, DemandPriority = @DemandPriority, 
                    Assumption = @Assumption, Dependency = @Dependency, InvestClassification = @InvestClassification, 
                    InvestmentScale = @InvestmentScale, PMOResponsible = @PMOResponsible, ITBizPartner = @ITBizPartner, 
                    PlannedStart = @PlannedStart, PlannedEnd = @PlannedEnd, FiscalYear = @FiscalYear, DateUpdated = @DateUpdated, 
                    CreatedBy = @CreatedBy, IsInactive = @IsInactive, Remarks = @Remarks
                WHERE DemandID = @DemandID";

            await connection.ExecuteAsync(updateQuery, demand);

            // Historyテーブルへの挿入
            const string historyInsertQuery = @"
                INSERT INTO DemandHistory (DemandID, Title, RequestedDept, RequestedBy, DemandOwner, DemandCategory, Objective, Proposal, 
                                           InvestmentDetails, BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, 
                                           InvestClassification, InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, 
                                           DateCreated, DateUpdated, CreatedBy, IsInactive, Remarks)
                VALUES (@DemandID, @Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, 
                        @InvestmentDetails, @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, 
                        @InvestClassification, @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, 
                        @DateCreated, @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(historyInsertQuery, demand);
        }

        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE Demand
                SET IsInactive = 1
                WHERE DemandID = @Id";

            await connection.ExecuteAsync(updateQuery, new { Id = id });

            // Historyテーブルへの挿入
            var demand = await GetById(id);
            if (demand != null)
            {
                const string historyInsertQuery = @"
                    INSERT INTO DemandHistory (DemandID, Title, RequestedDept, RequestedBy, DemandOwner, DemandCategory, Objective, Proposal, 
                                               InvestmentDetails, BusinessImpactIfNotConducted, RiskComment, DemandPriority, Assumption, Dependency, 
                                               InvestClassification, InvestmentScale, PMOResponsible, ITBizPartner, PlannedStart, PlannedEnd, FiscalYear, 
                                               DateCreated, DateUpdated, CreatedBy, IsInactive, Remarks)
                    VALUES (@DemandID, @Title, @RequestedDept, @RequestedBy, @DemandOwner, @DemandCategory, @Objective, @Proposal, 
                            @InvestmentDetails, @BusinessImpactIfNotConducted, @RiskComment, @DemandPriority, @Assumption, @Dependency, 
                            @InvestClassification, @InvestmentScale, @PMOResponsible, @ITBizPartner, @PlannedStart, @PlannedEnd, @FiscalYear, 
                            @DateCreated, @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

                await connection.ExecuteAsync(historyInsertQuery, demand);
            }
        }
    }
}
