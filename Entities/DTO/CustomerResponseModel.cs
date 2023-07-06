using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record CustomerResponseModel(
         string Name,
         int Identification,
         string City,
         string Address,
         string Id

    );
}
