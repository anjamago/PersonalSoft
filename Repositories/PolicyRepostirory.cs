
using Entities.Models;
using Repository.Interface;

namespace Repository
{
    public class PolicyRepostirory
    {
        private readonly IMongoRepositoryBase<Policy> _repositorie;
        private readonly IMongoRepositoryBase<Vehicles> _vehicleRepositorie;
        private readonly IMongoRepositoryBase<Customers> _customerRepositorie;

        public PolicyRepostirory(IMongoRepositoryBase<Policy> repositorie, IMongoRepositoryBase<Vehicles> vehicleRepositorie, IMongoRepositoryBase<Customers> customerRepositorie)
        {
            _repositorie = repositorie;
            _vehicleRepositorie = vehicleRepositorie;
            _customerRepositorie = customerRepositorie;
        }


        public async Task<Policy> GetPolicyPlaqueAsync(string policyNumberOrplaque)
        {
            var vehicle = _vehicleRepositorie.Queryable();
            var queryable = _repositorie.Queryable();

            if (vehicle.Where(w => w.Plaque.Equals(policyNumberOrplaque)).ToList().Any())
            {
                var result = from ve in vehicle
                             join po in queryable on ve.Id equals po.IdVehicule
                             select po;

                return result.ToList().First();
            }

            if (queryable.Where(w => w.policyNumber.Equals(policyNumberOrplaque)).ToList().Any())
            {
                var result = from po in queryable
                             join ve in vehicle on po.IdVehicule equals ve.Id
                             select po;
                return result.ToList().First();
            }

            return null;
        }

        public async Task Create(Policy policy)
            => await _repositorie.AddAsync(policy);
        
        public async Task<Policy> GetPolicyCustomerAsync(string idOrIdentification)
        {
            var queryable = _repositorie.Queryable();
            var customers = _customerRepositorie.Queryable().Where(w =>
                w.Identification.Equals(idOrIdentification) || w.Id.Equals(idOrIdentification));


            var result = from policy in queryable
                         join customer in customers on policy.IdCliente equals customer.Id
                         select policy;




            return result.ToList().First();
        }
    }
}
