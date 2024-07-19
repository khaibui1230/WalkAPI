using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;

namespace WalkAPI.Responsitories
{
    public class SQLWalkResponsitories : IWalksRespositories
    {
        private readonly NZWalkDbContext dbContext;

        public SQLWalkResponsitories(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existtingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existtingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existtingWalk);
            await dbContext.SaveChangesAsync();

            return existtingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? FilterOn = null, string? FilterQuerry = null
            ,string? sortBy= null , bool isAscending = true
            ,int pageNumber = 1, int pageSize = 1000)
        {
            // Initialize a queryable collection of Walks, including related Difficulty and Region entities.
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Check if both FilterOn and FilterQuerry are provided and not whitespace.
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterQuerry) == false)
            {
                // If FilterOn equals "Name" (case-insensitive), filter the walks by Name.
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(FilterQuerry));
                }
            }

            //Sortting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }                   
            }

            //pagination
            var skipResults = (pageNumber-1)* pageSize;

            // Execute the query and return the result as a list asynchronously.
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImgUrl = walk.WalkImgUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();

            return existingWalk;
        }

    }
}
