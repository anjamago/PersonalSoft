using Entities.Models;

namespace Repository.Interface
{
    public interface IVehicleRepository
    {
        Task Create(Vehicles vehicle);
    }
}