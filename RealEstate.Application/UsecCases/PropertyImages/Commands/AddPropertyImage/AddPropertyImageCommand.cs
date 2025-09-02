using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage
{
    public record AddPropertyImageCommand(
        int PropertyId,
        Stream ImageStream,
        string FileName,
        string ContentType
    ) : ICommand<ApplicationResponse<AddPropertyImageResponse>>;
}
