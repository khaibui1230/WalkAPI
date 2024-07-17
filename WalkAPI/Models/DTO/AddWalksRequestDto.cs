using System.ComponentModel.DataAnnotations;

namespace WalkAPI.Models.DTO
{
    public class AddWalksRequestDto
    {
        [Required]
        public string Name { get; set; }'
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
