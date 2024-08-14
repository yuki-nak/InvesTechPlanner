using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.Interfaces;
using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases
{
    public class DemandService(IDemandRepository demandRepository) : IDemandService
    {
        private readonly IDemandRepository _demandRepository = demandRepository;

        public void CreateDemand(DemandDto demandDto)
        {
            var demand = new Demand
            {
                Title = demandDto.Title,
                RequestedDept = demandDto.RequestedDept,
                RequestedBy = demandDto.RequestedBy,
                DemandOwner = demandDto.DemandOwner,
                DemandCategory = demandDto.DemandCategory,
                Objective = demandDto.Objective,
                Proposal = demandDto.Proposal,
                InvestmentDetails = demandDto.InvestmentDetails,
                BusinessImpactIfNotConducted = demandDto.BusinessImpactIfNotConducted,
                RiskComment = demandDto.RiskComment,
                DemandPriority = demandDto.DemandPriority,
                Assumption = demandDto.Assumption,
                Dependency = demandDto.Dependency,
                InvestClassification = demandDto.InvestClassification,
                InvestmentScale = demandDto.InvestmentScale,
                PMOResponsible = demandDto.PMOResponsible,
                ITBizPartner = demandDto.ITBizPartner,
                PlannedStart = demandDto.PlannedStart,
                PlannedEnd = demandDto.PlannedEnd,
                FiscalYear = demandDto.FiscalYear,
                CreatedBy = demandDto.CreatedBy,
                IsInactive = demandDto.IsInactive,
                Remarks = demandDto.Remarks
            };
            _demandRepository.Add(demand);
        }
    }
}
