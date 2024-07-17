using System.ComponentModel.DataAnnotations;

namespace WalkAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3 , ErrorMessage = "Code has to be minimum 3 character")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum 3 character")]
        public string Code { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Nam has to be maximum 100 character")]
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
