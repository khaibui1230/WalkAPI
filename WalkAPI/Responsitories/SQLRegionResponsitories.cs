using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;
using WalkAPI.Responsity;

namespace WalkAPI.Responsitories
{
    public class SQLRegionResponsitories : IRegionRespositories
    {
        private readonly NZWalkDbContext dbContext;

        public SQLRegionResponsitories(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var existRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (existRegion != null)
            {
                dbContext.Regions.Remove(existRegion);
                await dbContext.SaveChangesAsync();
                return existRegion;
            }
            return null;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existRegion == null)
            {
                return null;
            }

            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImgUrl = region.RegionImgUrl;

            await dbContext.SaveChangesAsync();
            return existRegion;
        }
    }
}
