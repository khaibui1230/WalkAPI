using WalkAPI.Models.Domain;

namespace WalkAPI.Responsitories
{
    public interface IWalksRespositories
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
