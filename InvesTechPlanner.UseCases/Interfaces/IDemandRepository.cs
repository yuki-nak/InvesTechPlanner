using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;
using System.Threading.Tasks;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandRepository
    {
        Task Add(Demand demand);
        Task Update(Demand demand);
        Task Delete(int demandId);
        Task<IEnumerable<Demand>> GetAll();
        Task<IEnumerable<Demand>> GetFilteredDemands(DemandFilterOptionsDto filterOptions);
        Task<Demand?> GetById(int id);
        Task<MemoryStream> ExportDemandsToCsv();
    }
}
