using System;
using Entities.Models;
using Repository.Interface;

namespace Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoRepositoryBase<Vehicles> _repository;
        public VehicleRepository(IMongoRepositoryBase<Vehicles> repository)
        {
            _repository = repository;
        }


        public async Task Create(Vehicles vehicle)
        {
            await _repository.AddAsync(vehicle);
        }

    }
}

