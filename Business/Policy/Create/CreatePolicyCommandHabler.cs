using Amazon.Runtime.Internal;
using Entities.Models;
using MediatR;
using Repository.Interface;
namespace Business.Create;

public class CreatePolicyCommandHabler : IRequestHandler<CreatePolicyCommand, List<string>> 
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPolicyRepository _policyRepository;
    private readonly IVehicleRepository _vehicleRepository;

    public CreatePolicyCommandHabler(ICustomerRepository customerRepository, IPolicyRepository policyRepository, IVehicleRepository vehicleRepository)
    {
        _customerRepository = customerRepository;
        _policyRepository = policyRepository;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<string>> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        List<string> erros = new();
        if (request.IdCustomer is not null)
        {
            var message = await ClientWhitPoliceAsync(request.IdCustomer) ? "El cliente cuenta con una poliza activa" : string.Empty;
            erros.Add(message);     
        }

        if(request.IdCustomer is null)
        {
            var message = await ClientWhitPoliceAsync(request.identification) ? "El cliente cuenta con una poliza activa" : string.Empty;
            erros.Add(message);
        }


        if (erros.Any(x=>x.Count() > 0))
        {
            return erros;
        }


        var customerModel = new Customers();
       

        if(request.IdCustomer is not null )
        {
            customerModel.Id = request.IdCustomer;
        }
        else
        {
            customerModel = new Customers()
            {
                Address = request.Address,
                Name = request.customerName,
                City = request.City,
                Identification = request.identification
            };
            await _customerRepository.AddCustomerAsync(customerModel);
        }

        var vehicleModel = new Vehicles()
        {
            IdUser = customerModel.Id,
            Model =  request.vehicleModel,
            Inspection =  request.whitInspection,
            Plaque =  request.plaque
        };
        await _vehicleRepository.Create(vehicleModel);
        var policeModel = new Policy()
        {
            policyNumber = request.policyNumber,
            CreateDate = DateTime.Now.ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IdCliente = customerModel.Id,
               IdVehicule = vehicleModel.Id
        };



        await _policyRepository.Create(policeModel);


        return new List<string>();

    
    }

    private async Task<bool> ClientWhitPoliceAsync(string idOrIdentification )
    {
        DateTime fechaActual = DateTime.Now;
        var policyCustomer = await _policyRepository.GetPolicyCustomerAsync(idOrIdentification);
        if(policyCustomer is null)
        {
            return false;
        }
        return (fechaActual >= DateTime.Parse(policyCustomer.StartDate) && fechaActual <= DateTime.Parse(policyCustomer.EndDate));
       
    }
}