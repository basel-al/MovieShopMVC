using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _accountService.Validate(model.Email, model.Password);
            if(user == null)
            {
                return Unauthorized("Wrong Email or Password");
            }
            return Ok(user);
        }


    }
}
