using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice
{
    public record UpdatePropertyPriceCommand(
        int PropertyId,
        decimal NewPrice
    ) : ICommand<ApplicationResponse<UpdatePropertyPriceResponse>>;
}
