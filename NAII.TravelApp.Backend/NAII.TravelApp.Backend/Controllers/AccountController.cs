using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NAII.TravelApp.Backend.Dto;
using NAII.TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email); if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false); if (result.Succeeded)
                {
                    string token = GetToken(user); 
                    return Created("", token); //returns only the token                    
                }
            }
            return BadRequest();
        }

        private String GetToken(User user)
        {      // Createthetoken
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null,null,claims,expires: DateTime.Now.AddMinutes(30),signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);}
        }
    }
