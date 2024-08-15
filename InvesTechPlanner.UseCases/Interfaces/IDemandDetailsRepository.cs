using InvesTechPlanner.Entities;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandDetailsRepository
    {
        Task Add(DemandDetail demandDetail);
    }
}
