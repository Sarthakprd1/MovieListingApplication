using AutoMapper;
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

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegister : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoginRegister _loginregister;
        private readonly IConfiguration _configuration;
        public LoginRegister(ILoginRegister loginRegister, IMapper mapper, IConfiguration configuration) 
        {
            _loginregister = loginRegister;
            _mapper = mapper;
            _configuration = configuration;
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
            if (ModelState.IsValid == false) 
            {
                return BadRequest();
            }
            if(await _loginregister.Login(loginUser))
            {
                string token = CreateToken(loginUser);
                return Ok(token);
            }

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


        private string CreateToken(IdentityUser user) 
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                //new Claim(ClaimTypes,);

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
