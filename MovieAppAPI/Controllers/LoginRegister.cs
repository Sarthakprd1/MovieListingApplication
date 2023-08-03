using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieAppAPI.DTO;
using MovieAppAPI.Services.Interfaces;
using MovieListing.Areas.Identity.Data;
using MovieListing.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MovieAppAPI.Services;
using System.Text;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegister : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoginRegister _loginregister;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _userManager;
        
        public LoginRegister(ILoginRegister loginRegister, IMapper mapper, IConfiguration configuration, SignInManager<IdentityUser> userManager) 
        {
            _loginregister = loginRegister;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDTO>> RegisterUser(RegisterDTO user)
        {
            var Idenuser = _mapper.Map<IdentityUser>(user);
            if (await _loginregister.RegisterUser(Idenuser))
            {
                return Ok("Successfully Done");
            }
            return BadRequest("Something Went Wrong");
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> RegisterUser(IdentityUser user)
        //{
        //    if (await _loginregister.RegisterUser(user))
        //    {
        //        return Ok("Successfully Done");
        //    }
        //    return BadRequest("Something Went Wrong");
        //}
        
        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO user)
        {
            var loginUser = _mapper.Map<IdentityUser>(user);
            var result = await _userManager.PasswordSignInAsync(loginUser.Email, loginUser.PasswordHash, false, false);

            if (result.Succeeded)
            {
                var token = CreateToken(loginUser);
                return Ok(token);
            }
            if (ModelState.IsValid == false) 
            {
                return BadRequest();
            }
            //if (await _loginregister.Login(loginUser))
            //{
            //    var token = CreateToken(loginUser);
            //    return Ok(token);
            //}

            //if (loginUser.Email != user.Email)
            //{
            //    return BadRequest("Email Not Found !");
            //}
            //if (loginUser.PasswordHash != user.PasswordHash)
            //{
            //    return BadRequest("Password Incorrect !");
            //}

            return BadRequest("Credentials not Found !");
        }


        private JwtTokens CreateToken(IdentityUser user) 
        {
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Email),
            //    new Claim(ClaimTypes.Role, "Admin")
            //};

            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            //    _configuration.GetSection("Jwt:Key").Value));

            ////for not being able to change the data from the client side
            //var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var token = new JwtSecurityToken(
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(1),
            //    issuer: _configuration.GetSection("Jwt:Issuer").Value,
            //    audience: _configuration.GetSection("Jwt:Audience").Value,
            //    signingCredentials: cred);

            //var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            //return jwt;

            // Generate and return JWT token if user is authenticated
            var tokenhandler = new JwtSecurityTokenHandler();
            var tkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescp = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescp);

            return new JwtTokens { Token = tokenhandler.WriteToken(token) };

        }

    }
}
