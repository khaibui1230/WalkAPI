using WalkAPI.Models.Domain;

namespace WalkAPI.Responsity
{
    public interface IRegionRespositories
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid Id);
        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid Id, Region region);
        Task<Region?> DeleteAsync(Guid Id);
    }
}
