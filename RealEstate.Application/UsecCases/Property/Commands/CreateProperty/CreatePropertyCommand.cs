using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public record CreatePropertyCommand(
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int OwnerId
    ) : ICommand<ApplicationResponse<CreatePropertyResponse>>;
}
