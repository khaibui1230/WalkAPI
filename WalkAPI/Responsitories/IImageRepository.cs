using WalkAPI.Models.Domain;

namespace WalkAPI.Responsitories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);    
    }
}
