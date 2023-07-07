using Entities.Models;

namespace Repository.Interface
{
    public interface IPlanRepository
    {
        Task<List<Plans>> GetList();
        Task Create(Plans plan);
    }
}
