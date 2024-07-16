using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WalkAPI.Models.Domain;

namespace WalkAPI.Data
{
    public class NZWalkDbContext : DbContext
    {
        //short cut : ctor
        public NZWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        //seeding the data using Entity frame
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base class's OnModelCreating method to ensure any base configuration is applied
            base.OnModelCreating(modelBuilder);

            // Seeding data to Difficulty table
            var difficulties = new List<Difficulty>()
            {
                // Create a new Difficulty object for "Easy" difficulty
                new Difficulty()
                {
                    Id = Guid.Parse("8626d6a1-9e34-4250-a94f-6a7203af9b80"), // Unique identifier for "Easy"
                    Name = "Easy" // Name of the difficulty
                },
                // Create a new Difficulty object for "Medium" difficulty
                new Difficulty()
                {
                    Id = Guid.Parse("6732d6ee-977c-4081-aff1-f3e7c9a4f6e8"), // Unique identifier for "Medium"
                    Name = "Medium" // Name of the difficulty
                },
                // Create a new Difficulty object for "Hard" difficulty
                new Difficulty()
                {
                    Id = Guid.Parse("51324663-b646-487b-b7b5-185a7e68fee7"), // Unique identifier for "Hard"
                    Name = "Hard" // Name of the difficulty
                },
            };

            // Use the HasData method to seed the initial data to the Difficulty entity
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seeding data for region 
            var regions = new List<Region>()
            {
                new Region() {
                    Id = Guid.Parse("be2e701f-2fe5-4e75-927e-8f1bbaee274b"),
                    Name = "Hieu",
                    Code = "BNH",
                    RegionImgUrl = "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0"


                },
                new Region() {
                    Id = Guid.Parse("1a95b499-1c1c-4f43-8d5f-f1e7dfd9bcc9"),
                    Name = "Khanh",
                    Code = "KV",
                    RegionImgUrl = null
                },
                new Region() {
                        Id = Guid.Parse("3dcd48d5-4ceb-47de-a99f-1b5284ce0b10"),
                        Name = "Huy",
                        Code = "VHH",
                        RegionImgUrl = "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0"
                }
            };
            //Seeding data to Difficulty table
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
