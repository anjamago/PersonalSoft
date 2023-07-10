
using Entities.DTO;
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


        public async Task<PolicyCustomerDto> GetPolicyPlaqueAsync(string policyNumberOrplaque)
        {
            var vehicleQueryable = _vehicleRepositorie.Queryable();
            var policyQueryable = _repositorie.Queryable();

            var result = policyQueryable.Join(
                    vehicleQueryable,
                    pol => pol.IdVehicule,
                    veh => veh.Id,
                    (pol, veh) => new PolicyCustomerDto()
                    {
                        Id = pol.Id,
                        policyNumber = pol.policyNumber,
                       CreateDate = pol.CreateDate,
                       StartDate = pol.StartDate,
                        EndDate = pol.EndDate,
                       IdCustomer = pol.IdCliente,
                       Plaque = veh.Plaque,
                       Model = veh.Model,
                       Inspection = veh.Inspection

                    }
                ).Where(w => w.Plaque.Equals(policyNumberOrplaque) || w.policyNumber.Equals(policyNumberOrplaque))
                .ToList();
        

            return result?.FirstOrDefault();
        }
        
        public async Task<List<PolicyCustomerDto>> GetAll()
        {
            var vehicleQueryable = _vehicleRepositorie.Queryable();
            var policyQueryable = _repositorie.Queryable();
            var customerQueryable = _customerRepositorie.Queryable();

            var result = from pol in policyQueryable
                join cus in customerQueryable on pol.IdCliente equals cus.Id
                join veh in vehicleQueryable on pol.IdVehicule equals veh.Id
                select new PolicyCustomerDto()
                {
                    Id = pol.Id,
                    policyNumber = pol.policyNumber,
                    CreateDate = pol.CreateDate,
                    StartDate = pol.StartDate,
                    EndDate = pol.EndDate,
                    IdCustomer = pol.IdCliente,
                    Plaque = veh.Plaque,
                    Model = veh.Model,
                    Inspection = veh.Inspection,
                    Name = cus.Name,
                    Identification = cus.Identification,
                    Address = cus.Address,
                    City = cus.City

                };
            
            return result.ToList();
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

            var item = result?.FirstOrDefault();
            var model =  item is null ? null: new Policy()
            {
                Id = item.Id,
                IdCliente = item.IdCliente,
                StartDate = item.StartDate,
                EndDate = item.EndDate,

            };
            return model;
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
