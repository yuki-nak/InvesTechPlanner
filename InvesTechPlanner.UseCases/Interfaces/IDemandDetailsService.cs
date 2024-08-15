using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandDetailsService
    {
        Task CreateDemandDetail(DemandDetailDto demandDetailDto);
    }
}
