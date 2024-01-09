using Car_Catalogue.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Car_Catalogue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }


        [HttpPost("create")]
        public async Task<ActionResult<Jwt>> CreateUser([FromBody] UserInfo userInfo)
        {
            //Sets email as username and email
            var user = new ApplicationUser { Email = userInfo.Email, UserName = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                return Ok(BuildToken(userInfo));
            }
            else return BadRequest("Invalid email or password");
        }

        [HttpPost("login")]
        public async Task<ActionResult<Jwt>> LoginUser([FromBody] UserInfo userInfo)
        {
            //Login the user using password
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(BuildToken(userInfo));
            }
            else return BadRequest("Invalid email or password");
        }

        private Jwt BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName , userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);
            return new Jwt(new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }
    }
}
