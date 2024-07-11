using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;
using WalkAPI.Models.DTO;

namespace WalkAPI.Controllers
{

    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;

        public RegionsController(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //return regionDomain from the database --RegionsDomain
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain model to Dtos
            var regionsDto = new List<RegionsDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionsDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImgUrl = regionDomain.RegionImgUrl
                });
            }

            //Return the Dto to client
            return Ok(regionsDto);
        }


        //GETSingle Region (Get Region by ID)
        //GET : https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //option 1 to find
            //var regions = dbContext.Regions.Find(id);

            //option 2 to find
            //Get region Domain model to database
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Convert Region Domain to Dtos
            var regionsDto = new RegionsDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImgUrl = regionDomain.RegionImgUrl
            };
            return Ok(regionsDto);
        }


        //POST  to  create  New Regions
        //POST : https//localhostL:portnumber/api/region
        //Different of 200 and 201 (Create a new Data)
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Covert/Map Dto to Domain
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImgUrl = addRegionRequestDto.RegionImgUrl
            };

            //Use Domain model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map domain model back to Dtos
            var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImgUrl = regionDomainModel.RegionImgUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        ////Update Region
        ////PUT : https://localhost:portnumber/api/regions/{id}
        //[HttpPut]
        //[Route("{id:Guid}")]
        //public IActionResult UpdateData([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        //{
        //    //check if region is exist
        //    var regionDomainModel = dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        //    if (regionDomainModel == null)
        //    {
        //        return NotFound();
        //    }
        //    //Map Dto to Domain Model
        //    regionDomainModel.Code = updateRegionRequestDto.Code;
        //    regionDomainModel.Name = updateRegionRequestDto.Name;
        //    regionDomainModel

           

        //}
    
    }
}
