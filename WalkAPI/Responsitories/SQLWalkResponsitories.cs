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
    }
}
