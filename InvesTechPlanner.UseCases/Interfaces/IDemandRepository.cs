using InvesTechPlanner.Entities;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandRepository
    {
        Task Add(Demand demand);
    }
}
