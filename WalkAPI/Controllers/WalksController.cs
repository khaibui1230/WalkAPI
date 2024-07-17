using AutoMapper; // For mapping between domain models and DTOs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkAPI.CustomActionFilter;
using WalkAPI.Models.Domain; // Namespace for domain models
using WalkAPI.Models.DTO; // Namespace for data transfer objects (DTOs)
using WalkAPI.Responsitories; // Namespace for repository interfaces

namespace WalkAPI.Controllers
{
    // Define the route for this controller as "api/walks" and make it an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        // Dependency injection for IMapper and IWalksRespositories
        private readonly IMapper mapper;
        private readonly IWalksRespositories walksRespositories;

        // Constructor to initialize IMapper and IWalksRespositories via dependency injection
        public WalksController(IMapper mapper, IWalksRespositories walksRespositories)
        {
            this.mapper = mapper; // Assign the injected IMapper to the local field
            this.walksRespositories = walksRespositories; // Assign the injected repository to the local field
        }

        // Endpoint to create a new walk
        // HTTP POST method: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {

            // Map the incoming DTO to the domain model
            var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);

            // Use the repository to create the new walk asynchronously
            await walksRespositories.CreateAsync(walkDomainModel);

            // Return an OK response
            return Ok(mapper.Map<WalksDto>(walkDomainModel));



        }

        //GET :Walk
        //GET  : /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDomainModel = await walksRespositories.GetAllAsync();
            //map domain to Dto
            return Ok(mapper.Map<List<WalksDto>>(walkDomainModel));
        }

        //Get byID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRespositories.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map domain to Dto
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }

        //Update by id
        //PUT : /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalksRequestDto updateWalksRequestDto)
        {

            //Map dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalksRequestDto);

            walkDomainModel = await walksRespositories.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }

        //Remove by id
        //DELETE :"/api/Walk/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteDomainModel = await walksRespositories.DeleteAsync(id);

            if (deleteDomainModel == null)
            { return NotFound(); }

            //Map domain to dto
            return Ok(mapper.Map<WalksDto>(deleteDomainModel));
        }
    }

}
