using Dapper;
using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.Interfaces;

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
    }
}
