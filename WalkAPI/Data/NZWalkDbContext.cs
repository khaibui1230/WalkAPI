using Microsoft.EntityFrameworkCore;
using WalkAPI.Models.Domain;

namespace WalkAPI.Data
{
    public class NZWalkDbContext : DbContext
    {
        //short cut : ctor
        public NZWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
                        
        }

        public DbSet<Difficulty> Difficulties{ get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
