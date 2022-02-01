using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            try
            {
                var user = await _accountService.Register(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized("Email already taken");
            }

        }
        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var user = await _accountService.emailExists(email);
            if (user == true)
            {
                return Ok();
            }
            return Unauthorized("Email does not exist");

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _accountService.Validate(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized("Wrong Email or Password");
            }
            //generate jwt
            /*var token = GenerateJWT(user);*/
            var token = GenerateJWT(user);
            return Ok(new { token = token });
        }
        private string GenerateJWT(UserLoginResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),             
                new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("language", "english")
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiration = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expiration,
                SigningCredentials = credentials,
                Issuer = _configuration["Issue"],
                Audience = _configuration["Audience"]
            };
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }



}
    