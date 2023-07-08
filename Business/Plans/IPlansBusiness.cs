using Business.Create;
using Entities;
using Entities.DTO;

namespace Business
{
    public interface IPlansBusiness
    {
        Task<ResponseBase<List<string>>> Create(PlansCommand plan);
        Task<ResponseBase<List<Plan>>> GetList();
    }
}