using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;
using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;
using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;
using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;
using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new property building
        /// </summary>
        /// <param name="command">Property creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created property information</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePropertyResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(
                nameof(GetPropertiesWithFilters),
                new { propertyId = result.Value!.PropertyId },
                result);
        }

        /// <summary>
        /// Updates an existing property
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="command">Property update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated property information</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(UpdatePropertyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var command = new UpdatePropertyCommand(
                id,
                request.Name,
                request.Address,
                request.Price,
                request.CodeInternal,
                request.Year,
                request.OwnerId);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Changes the price of a property
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="request">New price information</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Price change information</returns>
        [HttpPut("{id:int}/price")]
        [ProducesResponseType(typeof(UpdatePropertyPriceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePropertyPrice(int id, [FromBody] UpdatePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            var command = new UpdatePropertyPriceCommand(id, request.NewPrice);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Adds an image to a property
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <param name="file">Image file</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Added image information</returns>
        [HttpPost("{id:int}/images")]
        [ProducesResponseType(typeof(AddPropertyImageResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPropertyImage(int id, IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var command = new AddPropertyImageCommand(
                id,
                file.OpenReadStream(),
                file.FileName,
                file.ContentType);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(
                nameof(GetPropertiesWithFilters),
                new { propertyId = id },
                result);
        }

        /// <summary>
        /// Lists properties with filters
        /// </summary>
        /// <param name="name">Property name filter</param>
        /// <param name="address">Property address filter</param>
        /// <param name="minPrice">Minimum price filter</param>
        /// <param name="maxPrice">Maximum price filter</param>
        /// <param name="minYear">Minimum year filter</param>
        /// <param name="maxYear">Maximum year filter</param>
        /// <param name="ownerId">Owner ID filter</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of properties with filters applied</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetPropertiesWithFiltersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesWithFilters(
            [FromQuery] string? name = null,
            [FromQuery] string? address = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] int? minYear = null,
            [FromQuery] int? maxYear = null,
            [FromQuery] int? ownerId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPropertiesWithFiltersQuery(
                name, address, minPrice, maxPrice,
                minYear, maxYear, ownerId,
                pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
