using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdateProperty
{
    public record UpdatePropertyCommand(
        int Id,
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int OwnerId
    ) : ICommand<ApplicationResponse<UpdatePropertyResponse>>;
}
