using WalkAPI.Models.Domain;

namespace WalkAPI.Responsitories
{
    public interface IWalksRespositories
    {
        Task<Walk> CreateAsync(Walk walk);
        // cap nhat them filter cho ham hine
        Task<List<Walk>> GetAllAsync(string? FilterOn= null, string? FilterQuerry = null
            ,string? sortBy = null, bool isAscending = true
            ,int pageNumber = 1, int pageSize = 1000); 
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
