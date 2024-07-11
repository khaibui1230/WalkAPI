namespace WalkAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm {  get; set; }
        public string? WalkImgUrl {  get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //navigation properties
        public Difficulty Difficulty { get; set; }

        //short cut prop
        public Region Region { get; set; }


    }
}
