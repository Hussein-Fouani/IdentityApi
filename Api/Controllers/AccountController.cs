using Api.Models;
using Api.Models.Dto.Account;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService jwtService;
        private readonly SignInManager<Users> signManager;
        private readonly UserManager<Users> userManager;

        public AccountController(JwtService jwtService,SignInManager<Users> signManager,UserManager<Users> userManager)
        {
            this.jwtService = jwtService;
            this.signManager = signManager;
            this.userManager = userManager;
        }


        #region Private HelperMethod
        private UserDto CreateApplicationUserDto(Users user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Jwt = jwtService.GenerateJWT(user)
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null;
        }
        #endregion

        [Authorize]
        [HttpPost("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return CreateApplicationUserDto(user);
        }
        


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid Credentials");
            }
            if(user.EmailConfirmed== false)
            {
                return Unauthorized ("Email is not confirmed");
            }

            var result = await signManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded)
            {
                return Unauthorized("Invalid Credentials");
            }
            
            
                return Ok(CreateApplicationUserDto(user));
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        { 
            if (await CheckEmailExistsAsync(registerDto.Email))
            {
                return BadRequest("Email Already Exists");
            }
            var user = new Users
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                EmailConfirmed = true
            };  
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(CreateApplicationUserDto(user));
        }
    }
}
