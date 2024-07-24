using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WalkAPI.CustomActionFilter;
using WalkAPI.Data;
using WalkAPI.Mapping;
using WalkAPI.Models.Domain;
using WalkAPI.Models.DTO;
using WalkAPI.Responsity;

namespace WalkAPI.Controllers
{

    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        private readonly IRegionRespositories regionRespositories;
        private readonly IMapper mapper;

        public RegionsController(NZWalkDbContext dbContext, IRegionRespositories regionRespositories
            , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRespositories = regionRespositories;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //return regionDomain from the database --RegionsDomain
            var regionsDomain = await regionRespositories.GetAllAsync(); // change to async

            //Map Domain model to Dtos
            //var regionsDto = new List<RegionsDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionsDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImgUrl = regionDomain.RegionImgUrl
            //    });
            //}

            //Return the Dto to client
            return Ok(mapper.Map<List<RegionsDto>>(regionsDomain));
        }


        //GETSingle Region (Get Region by ID)
        //GET : https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //option 1 to find
            //var regions = dbContext.Regions.Find(id);

            //option 2 to find
            //Get region Domain model to database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            ////Convert Region Domain to Dtos
            //var regionsDto = new RegionsDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImgUrl = regionDomain.RegionImgUrl
            //};
            return Ok(mapper.Map<List<RegionsDto>>(regionDomain));
        }


        //POST  to  create  New Regions
        //POST : https//localhostL:portnumber/api/region
        //Different of 200 and 201 (Create a new Data)
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Covert/Map Dto to Domain
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            //    new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImgUrl = addRegionRequestDto.RegionImgUrl
            //};

            //Use Domain model to create Region
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            // change implement 
            await regionRespositories.CreateAsync(regionDomainModel);

            ////Map domain model back to Dtos
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);
            //new RegionsDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImgUrl = regionDomainModel.RegionImgUrl
            //};

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);


        }

        //Update Region
        //PUT : https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateData([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //conver to Dto
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            //    new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImgUrl = updateRegionRequestDto.RegionImgUrl

            //};
            // Check if a region with the given ID exists in the database
            //var regionDomainModel = await regionRespositories.GetByIdAsync(id);
            regionDomainModel = await regionRespositories.UpdateAsync(id, regionDomainModel);

            // If the region does not exist, return a 404 Not Found response
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //// Map the properties from the DTO to the domain model
            //regionDomainModel.Code = updateRegionRequestDto.Code; // Update the Code
            //regionDomainModel.Name = updateRegionRequestDto.Name; // Update the Name
            //regionDomainModel.RegionImgUrl = updateRegionRequestDto.RegionImgUrl; // Update the Image URL

            //// Save the changes to the database
            //await dbContext.SaveChangesAsync();

            // Convert the updated domain model back to a DTO for the response
            //var regionDto = new RegionsDto
            //{
            //    Id = regionDomainModel.Id, // Set the ID
            //    Name = regionDomainModel.Name, // Set the Name
            //    Code = regionDomainModel.Code, // Set the Code
            //    RegionImgUrl = regionDomainModel.RegionImgUrl // Set the Image URL
            //};

            // Return a 200 OK response with the updated DTO
            return Ok(mapper.Map<RegionsDto>(regionDomainModel));



        }

        //Delete 
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteData([FromRoute] Guid id)
        {
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRespositories.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            // Convert the updated domain model back to a DTO for the response
            //var regionDto = new RegionsDto
            //{
            //    Id = regionDomainModel.Id, // Set the ID
            //    Name = regionDomainModel.Name, // Set the Name
            //    Code = regionDomainModel.Code, // Set the Code
            //    RegionImgUrl = regionDomainModel.RegionImgUrl // Set the Image URL
            //};

            return Ok(mapper.Map<RegionsDto>(regionDomainModel));
        }
    }


}

