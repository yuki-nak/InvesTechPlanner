using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;
using InvesTechPlanner.UseCases.Interfaces;

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

        public async Task UpdateDemandDetail(DemandDetailDto demandDetailDto)
        {
            var demandDetail = new DemandDetail
            {
                DemandDetailsId = demandDetailDto.DemandDetailsId ?? 0,
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
                DateUpdated = DateTime.Now,
                CreatedBy = demandDetailDto.CreatedBy,
                IsInactive = demandDetailDto.IsInactive,
                Remarks = demandDetailDto.Remarks
            };

            await _demandDetailsRepository.Update(demandDetail);
        }

        public async Task DeleteDemandDetail(int demandDetailsId)
        {
            await _demandDetailsRepository.Delete(demandDetailsId);
        }

        public async Task<IEnumerable<DemandDetailDto>> GetDemandDetailsByDemandId(int demandId)
        {
            var demandDetails = await _demandDetailsRepository.GetDetailedDemandID(demandId);
            return demandDetails.Select(d => new DemandDetailDto
            {
                DemandDetailsId = d.DemandDetailsId,
                DemandId = (int)d.DemandId,
                Title = d.Title,
                Description = d.Description,
                SpendDept = d.SpendDept,
                ExpenseType = d.ExpenseType,
                SpendCategory = d.SpendCategory,
                CostType = d.CostType,
                CurrentCost = d.CurrentCost,
                Year0 = d.Year0,
                Year1 = d.Year1,
                Year2 = d.Year2,
                Year3 = d.Year3,
                Year4 = d.Year4,
                Year5 = d.Year5,
                Remarks = d.Remarks,
                IsInactive = d.IsInactive
            }).ToList();
        }

        public async Task<IEnumerable<DemandDetailDto>> GetDetailedDemandID(int demandId)
        {
            var demandDetails = await _demandDetailsRepository.GetDetailedDemandID(demandId);
            return demandDetails.Select(d => new DemandDetailDto
            {
                DemandDetailsId = d.DemandDetailsId,
                DemandId = (int)d.DemandId,
                Title = d.Title,
                Description = d.Description,
                SpendDept = d.SpendDept,
                ExpenseType = d.ExpenseType,
                SpendCategory = d.SpendCategory,
                CostType = d.CostType,
                CurrentCost = d.CurrentCost,
                Year0 = d.Year0,
                Year1 = d.Year1,
                Year2 = d.Year2,
                Year3 = d.Year3,
                Year4 = d.Year4,
                Year5 = d.Year5,
                Remarks = d.Remarks,
                IsInactive = d.IsInactive
            }).ToList();
        }
        public async Task<MemoryStream> ExportDemandDetailsToCsv(int demandId)
        {
            return await _demandDetailsRepository.ExportDemandDetailsToCsv(demandId);
        }

        public async Task<Dictionary<string, Dictionary<string, SummaryDto>>> GetSummaryByCostType(int demandId)
        {
            return await _demandDetailsRepository.GetSummaryByCostType(demandId);
        }

        public int? CalculatePaybackPeriod(SummaryDto summary)
        {
            decimal cumulativeCashFlow = 0;

            decimal[] yearlyInvestments = {
            summary.Year0,
            summary.Year1,
            summary.Year2,
            summary.Year3,
            summary.Year4,
            summary.Year5
        };

            for (int year = 0; year < yearlyInvestments.Length; year++)
            {
                cumulativeCashFlow += yearlyInvestments[year] - summary.Current;

                if (cumulativeCashFlow >= 0)
                {
                    return year; // 累積キャッシュフローがゼロまたは正になる最初の年を返す
                }
            }

            return null; // 期間内に回収されない場合
        }

    }
}
