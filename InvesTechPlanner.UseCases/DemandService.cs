﻿using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;
using InvesTechPlanner.UseCases.Interfaces;

namespace InvesTechPlanner.UseCases
{
    public class DemandService(IDemandRepository demandRepository) : IDemandService
    {
        private readonly IDemandRepository _demandRepository = demandRepository;

        public async Task CreateDemand(DemandDto demandDto)
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
                Status = demandDto.Status,
                InvestClassification = demandDto.InvestClassification,
                InvestmentScale = demandDto.InvestmentScale,
                PMOResponsible = demandDto.PMOResponsible,
                ITBizPartner = demandDto.ITBizPartner,
                PlannedStart = demandDto.PlannedStart,
                PlannedEnd = demandDto.PlannedEnd,
                FiscalYear = demandDto.FiscalYear,
                CreatedBy = demandDto.CreatedBy,
                IsInactive = demandDto.IsInactive,
                Remarks = demandDto.Remarks,
                DocUrl = demandDto.DocUrl
            };
            await _demandRepository.Add(demand);
        }

        public async Task UpdateDemand(DemandDto demandDto)
        {
            var demand = new Demand
            {
                DemandId = demandDto.DemandId,
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
                Status = demandDto.Status,
                InvestClassification = demandDto.InvestClassification,
                InvestmentScale = demandDto.InvestmentScale,
                PMOResponsible = demandDto.PMOResponsible,
                ITBizPartner = demandDto.ITBizPartner,
                PlannedStart = demandDto.PlannedStart,
                PlannedEnd = demandDto.PlannedEnd,
                FiscalYear = demandDto.FiscalYear,
                DateUpdated = DateTime.Now,
                Remarks = demandDto.Remarks,
                IsInactive = demandDto.IsInactive,
                DocUrl = demandDto.DocUrl
            };

            await _demandRepository.Update(demand);
        }

        public async Task DeleteDemand(int demandId)
        {
            await _demandRepository.Delete(demandId);
        }

        public async Task<List<DemandDto>> GetDemandListItems()
        {
            var demands = await _demandRepository.GetAll();
            return demands.Select(d => new DemandDto
            {
                DemandId = d.DemandId,
                Title = d.Title,
                FiscalYear = d.FiscalYear,
                DemandCategory = d.DemandCategory,
                RequestedDept = d.RequestedDept,
                Status = d.Status,
                InvestmentScale = d.InvestmentScale,
                DemandPriority = d.DemandPriority,
                DocUrl = d.DocUrl
            }).ToList();
        }

        public async Task<DemandFilterOptionsDto> GetOptionsForFilters()
        {
            var demands = await _demandRepository.GetAll();

            var filterOptions = new DemandFilterOptionsDto
            {
                FiscalYears = demands.Select(d => d.FiscalYear?.ToString() ?? "Unknown").Distinct().OrderBy(y => y).ToList<string?>(),
                DemandCategories = demands.Select(d => d.DemandCategory ?? "Unknown").Distinct().OrderBy(c => c).ToList<string?>(),
                RequestedDepts = demands.Select(d => d.RequestedDept ?? "Unknown").Distinct().OrderBy(d => d).ToList<string?>(),
                Statuses = demands.Select(d => d.Status ?? "Unknown").Distinct().OrderBy(s => s).ToList<string?>(),
                InvestmentScales = demands.Select(d => d.InvestmentScale ?? "Unknown").Distinct().OrderBy(s => s).ToList<string?>(),
                DemandPriorities = demands.Select(d => d.DemandPriority ?? "Unknown").Distinct().OrderBy(p => p).ToList<string?>(),
            };

            return filterOptions;
        }

        public async Task<List<DemandDto>> GetFilteredDemands(DemandFilterOptionsDto filterOptions)
        {
            var demands = await _demandRepository.GetFilteredDemands(filterOptions);
            return demands.Select(d => new DemandDto
            {
                DemandId = d.DemandId,
                Title = d.Title,
                FiscalYear = d.FiscalYear,
                DemandCategory = d.DemandCategory,
                RequestedDept = d.RequestedDept,
                Status = d.Status,
                InvestmentScale = d.InvestmentScale,
                DemandPriority = d.DemandPriority
            }).ToList();
        }

        public async Task<DemandDto?> GetDemandById(int demandId)
        {
            var demand = await _demandRepository.GetById(demandId);
            if (demand == null) return null;

            return new DemandDto
            {
                DemandId = demand.DemandId,
                Title = demand.Title,
                RequestedDept = demand.RequestedDept,
                RequestedBy = demand.RequestedBy,
                DemandOwner = demand.DemandOwner,
                DemandCategory = demand.DemandCategory,
                Status = demand.Status,
                FiscalYear = demand.FiscalYear,
                Objective = demand.Objective,
                Proposal = demand.Proposal,
                InvestmentDetails = demand.InvestmentDetails,
                BusinessImpactIfNotConducted = demand.BusinessImpactIfNotConducted,
                RiskComment = demand.RiskComment,
                Remarks = demand.Remarks,
                DocUrl = demand.DocUrl
            };
        }

        public async Task<MemoryStream> ExportDemandsToCsv()
        {
            return await _demandRepository.ExportDemandsToCsv();
        }
    }
}
