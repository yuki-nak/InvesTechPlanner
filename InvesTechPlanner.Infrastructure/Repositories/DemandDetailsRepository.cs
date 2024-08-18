using CsvHelper;
using Dapper;
using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;
using InvesTechPlanner.UseCases.Interfaces;
using System.Globalization;
using System.Text;

namespace InvesTechPlanner.Infrastructure.Repositories
{
    public class DemandDetailsRepository(IDatabaseConnectionFactory connectionFactory) : IDemandDetailsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory = connectionFactory;

        public async Task Add(DemandDetail demandDetail)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string insertQuery = @"
                INSERT INTO DemandDetails (DemandId, Title, Description, SpendDept, ExpenseType, SpendCategory, CostType,
                                           CurrentCost, Year0, Year1, Year2, Year3, Year4, Year5, DateCreated, DateUpdated, CreatedBy, 
                                           IsInactive, Remarks)
                VALUES (@DemandId, @Title, @Description, @SpendDept, @ExpenseType, @SpendCategory, @CostType, 
                        @CurrentCost, @Year0, @Year1, @Year2, @Year3, @Year4, @Year5, @DateCreated, @DateUpdated, @CreatedBy, 
                        @IsInactive, @Remarks)";

            await connection.ExecuteAsync(insertQuery, demandDetail);

            const string historyInsertQuery = @"
                INSERT INTO DemandDetailsHistory (DemandDetailsId, DemandId, Title, Description, SpendDept, ExpenseType, SpendCategory, 
                                                  CostType, CurrentCost, Year0, Year1, Year2, Year3, Year4, Year5, DateCreated, 
                                                  DateUpdated, CreatedBy, IsInactive, Remarks)
                VALUES (@DemandDetailsId, @DemandId, @Title, @Description, @SpendDept, @ExpenseType, @SpendCategory, 
                        @CostType, @CurrentCost, @Year0, @Year1, @Year2, @Year3, @Year4, @Year5, @DateCreated, 
                        @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(historyInsertQuery, demandDetail);
        }

        public async Task<DemandDetail?> GetById(int demandDetailsId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM DemandDetails WHERE DemandDetailsId = @DemandDetailsId";
            return await connection.QuerySingleOrDefaultAsync<DemandDetail>(query, new { DemandDetailsId = demandDetailsId });
        }

        public async Task<IEnumerable<DemandDetail>> GetDetailedDemandID(int demandId)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM DemandDetails WHERE DemandId = @DemandId";
            return await connection.QueryAsync<DemandDetail>(query, new { DemandId = demandId });
        }

        public async Task<IEnumerable<DemandDetail>> GetDemandDetailsByDemandId(int demandId)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = "SELECT * FROM DemandDetails WHERE DemandId = @DemandId AND IsInactive != 1";
            return await connection.QueryAsync<DemandDetail>(query, new { DemandId = demandId });
        }

        public async Task Update(DemandDetail demandDetail)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE DemandDetails
                SET Title = @Title, Description = @Description, SpendDept = @SpendDept, ExpenseType = @ExpenseType, SpendCategory = @SpendCategory, 
                    CostType = @CostType, CurrentCost = @CurrentCost, Year0 = @Year0, Year1 = @Year1, Year2 = @Year2, 
                    Year3 = @Year3, Year4 = @Year4, Year5 = @Year5, DateUpdated = @DateUpdated, CreatedBy = @CreatedBy, IsInactive = @IsInactive, 
                    Remarks = @Remarks
                WHERE DemandDetailsId = @DemandDetailsId";

            await connection.ExecuteAsync(updateQuery, demandDetail);

            const string historyInsertQuery = @"
                INSERT INTO DemandDetailsHistory (DemandDetailsId, DemandId, Title, Description, SpendDept, ExpenseType, SpendCategory, 
                                                  CostType, CurrentCost, Year0, Year1, Year2, Year3, Year4, Year5, DateCreated, 
                                                  DateUpdated, CreatedBy, IsInactive, Remarks)
                VALUES (@DemandDetailsId, @DemandId, @Title, @Description, @SpendDept, @ExpenseType, @SpendCategory, 
                        @CostType, @CurrentCost, @Year0, @Year1, @Year2, @Year3, @Year4, @Year5, @DateCreated, 
                        @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

            await connection.ExecuteAsync(historyInsertQuery, demandDetail);
        }

        public async Task Delete(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string updateQuery = @"
                UPDATE DemandDetails
                SET IsInactive = 1
                WHERE DemandDetailsId = @Id";

            await connection.ExecuteAsync(updateQuery, new { Id = id });

            var demandDetail = await GetById(id);
            if (demandDetail != null)
            {
                const string historyInsertQuery = @"
                    INSERT INTO DemandDetailsHistory (DemandDetailsId, DemandId, Title, Description, SpendDept, ExpenseType, SpendCategory, 
                                                      CostType, CurrentCost, Year0, Year1, Year2, Year3, Year4, Year5, DateCreated, 
                                                      DateUpdated, CreatedBy, IsInactive, Remarks)
                    VALUES (@DemandDetailsId, @DemandId, @Title, @Description, @SpendDept, @ExpenseType, @SpendCategory, 
                            @CostType, @CurrentCost, @Year0, @Year1, @Year2, @Year3, @Year4, @Year5, @DateCreated, 
                            @DateUpdated, @CreatedBy, @IsInactive, @Remarks)";

                await connection.ExecuteAsync(historyInsertQuery, demandDetail);
            }
        }

        public async Task<MemoryStream> ExportDemandDetailsToCsv(int demandId)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM DemandDetails WHERE DemandId = @DemandId AND IsInactive = 0";
            var demandDetails = await connection.QueryAsync<DemandDetail>(sql, new { DemandId = demandId });

            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(demandDetails);
            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<Dictionary<string, Dictionary<string, SummaryDto>>> GetSummaryByCostType(int demandId)
        {
            var demandDetails = await GetDemandDetailsByDemandId(demandId);

            var summaryData = demandDetails
                .GroupBy(d => d.CostType)
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(d => d.ExpenseType)
                          .ToDictionary(
                              e => e.Key,
                              e => new SummaryDto
                              {
                                  Current = e.Sum(d => d.CurrentCost ?? 0),
                                  Year0 = e.Sum(d => d.Year0 ?? 0),
                                  Year1 = e.Sum(d => d.Year1 ?? 0),
                                  Year2 = e.Sum(d => d.Year2 ?? 0),
                                  Year3 = e.Sum(d => d.Year3 ?? 0),
                                  Year4 = e.Sum(d => d.Year4 ?? 0),
                                  Year5 = e.Sum(d => d.Year5 ?? 0),
                              }
                          )
                );

            return summaryData;
        }


    }
}
