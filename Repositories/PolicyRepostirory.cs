
using Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Repository.Interface;

namespace Repository
{
    public class PolicyRepostirory : IPolicyRepository
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
                             select new Policy { Id = po.Id };

                return result.ToList().First();
            }

            return null;
        }

        public async Task Create(Policy policy)
            => await _repositorie.AddAsync(policy);

        public async Task<Policy> GetPolicyCustomerAsync(string idOrIdentification)
        {
            var queryable = _repositorie.Queryable();
            var customers = _customerRepositorie.Queryable();

            Func<PolicisCustomer, bool> func =   ObjectId.TryParse(idOrIdentification, out _) ? 
                 (w) => w.IdCliente.Equals(idOrIdentification) 
                 :(w) => w.Identification.Equals(idOrIdentification);

            var result = queryable.Join(
               customers,
               (pol) => pol.IdCliente,
               (customer) => customer.Id,
               (pol, customer) => new  PolicisCustomer(  
                   pol.IdCliente,
                   pol.Id,
                   customer.Identification,
                   pol.StartDate,
                   pol.EndDate
                )
           ).Where(func)
           .ToList();

            var item = result.FirstOrDefault();

            return new Policy()
            {
                Id = item.Id,
                IdCliente = item.IdCliente,
                StartDate = item.StartDate,
                EndDate = item.EndDate,

            };
        }
    }

    public  class PolicisCustomer
    {
        public PolicisCustomer(string idCliente, string id, string identification, string startDate, string endDate)
        {
            IdCliente = idCliente;
            Id = id;
            Identification = identification;
            StartDate = startDate;
            EndDate = endDate;
        }

        public  string IdCliente { get; set; }
        public string Id { get; set; }
        public string Identification { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
