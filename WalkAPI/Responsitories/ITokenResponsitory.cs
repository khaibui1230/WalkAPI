using Microsoft.AspNetCore.Identity;

namespace WalkAPI.Responsitories
{
    public interface ITokenResponsitory
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
