
using Entities.Models;
using Repository.Interface;

namespace Repository
{
    public class PolicyRepostirory
    {
        private readonly IMongoRepositoryBase<Policy> _repositorie;
        private readonly IMongoRepositoryBase<Vehicles> _vehicleRepositorie;

        public PolicyRepostirory(IMongoRepositoryBase<Policy> repositorie, IMongoRepositoryBase<Vehicles> vehicleRepositorie)
        {
            _repositorie = repositorie;
            _vehicleRepositorie = vehicleRepositorie;
        }


        public Task<Policy> GetAsync(string policyNumberOrplaque)
        {
            var vehicle = _vehicleRepositorie.Queryable();
            var queryable = _repositorie.Queryable();
           
            var query = queryable.GroupJoin(
                vehicle,
                    policy => policy.IdVehicule,
                    vhc => vhc.Id,
                    (policy, vhc) => new { Policys = policy, Vehicle = vhc }
                )
                .Where(w => w.Policys.Equals(policyNumberOrplaque) || w.Vehicle.Equals(policyNumberOrplaque));

            return query;

        }
    }
}
