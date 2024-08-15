using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.Interfaces;
using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases
{
    public class DemandDetailsService(IDemandDetailsRepository demandDetailsRepository) : IDemandDetailsService
    {
        private readonly IDemandDetailsRepository _demandDetailsRepository = demandDetailsRepository;

        public async Task CreateDemandDetail(DemandDetailDto demandDetailDto)
        {
            var demandDetail = new DemandDetail
            {
                DemandId = demandDetailDto.DemandId,
                Title = demandDetailDto.Title,
                Description = demandDetailDto.Description,
                SpendDept = demandDetailDto.SpendDept,
                ExpenseType = demandDetailDto.ExpenseType,
                SpendCategory = demandDetailDto.SpendCategory,
                CostType = demandDetailDto.CostType,
                CurrentCost = demandDetailDto.CurrentCost,
                Year0 = demandDetailDto.Year0,
                Year1 = demandDetailDto.Year1,
                Year2 = demandDetailDto.Year2,
                Year3 = demandDetailDto.Year3,
                Year4 = demandDetailDto.Year4,
                Year5 = demandDetailDto.Year5,
                CreatedBy = demandDetailDto.CreatedBy,
                IsInactive = demandDetailDto.IsInactive,
                Remarks = demandDetailDto.Remarks
            };
            await _demandDetailsRepository.Add(demandDetail);
        }
    }
}
