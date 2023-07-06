using Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Customer.Query
{
    public  class GetCustomerByIdRequest:IRequest<CustomerResponseModel>
    {
        public string Id { get; set; }
    }
}
