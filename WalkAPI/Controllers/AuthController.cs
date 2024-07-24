using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalkAPI.Models.DTO;
using WalkAPI.Responsitories;

namespace WalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenResponsitory tokenResponsitory;

        public AuthController(UserManager<IdentityUser> userManager, ITokenResponsitory tokenResponsitory)
        {
            this.userManager = userManager;
            this.tokenResponsitory = tokenResponsitory;
        }

        //Post : api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add Role to user 
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registed !! Please login");
                    }
                }
            }

            // Lấy chi tiết lỗi
            var errors = identityResult.Errors;
            return BadRequest(new { Errors = errors });

        }

        //POST : api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            //check the user name 
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            // check user name not null
            if (user != null)
            {
                // check the password
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get role for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //create token
                        var jwtToken = tokenResponsitory.CreateJWTToken(user, roles.ToList());
                        var respond = new LoginResponeDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(respond);
                    }
                }
            }

            return BadRequest("Email or password incorrect");
        }
    }
}
