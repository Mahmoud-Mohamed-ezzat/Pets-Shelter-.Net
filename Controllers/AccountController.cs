using Animal2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Animal2.Dto.Account;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Animal2.Service;
using Microsoft.AspNetCore.Authorization;

namespace Animal2.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly TokenService _Token;
        private readonly SignInManager<Customer> _signInManager;


        public AccountController(UserManager<Customer> userManager, TokenService Token, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _Token = Token;
            _signInManager = signInManager;

        }

        [AllowAnonymous] //Authorize For Anyone
        [HttpPost("RegisterAdopter")]
        public async Task<IActionResult> RegisterAdopter([FromBody] RegisterDto registermodel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var Adopter = new Customer
                {
                    UserName = registermodel.UserName,
                    Email = registermodel.Email,
                    PhoneNumber = registermodel.PhoneNumber,
                    UserCategoryId = 2,
                };
                var CreatedAdopter = await _userManager.CreateAsync(Adopter, registermodel.Password);
                /* .CreateAsync(Adopter,registermodel.Password);*/
                if (CreatedAdopter.Succeeded)
                {
                    var RoleResult = await _userManager.AddToRoleAsync(Adopter, "Adopter");
                    if (RoleResult.Succeeded)
                    {
                        return Ok(
                        new NewUserDto
                        {
                            Email = Adopter.Email,
                            UserName = Adopter.UserName,
                            Token = _Token.CreateToken(Adopter),
                            Role = ["Adpter"]

                        });
                    }
                    else return StatusCode(500, RoleResult.Errors.ToString());
                }
                else
                {
                    return StatusCode(500, CreatedAdopter.Errors.ToString());
                }


            }
            catch (Exception e)
            {
                return StatusCode(500, e);

            }

        }


        [AllowAnonymous]
        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customer = await _userManager.Users.FirstOrDefaultAsync(c => c.Email.ToLower() == loginDto.Email.ToLower());
            if (customer == null) return Unauthorized("User Notfound");
            var result = await _signInManager.CheckPasswordSignInAsync(customer, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Email and/or Password incorrect ");
            return Ok(
                new NewUserDto
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Token = _Token.CreateToken(customer),
                    Role = await _userManager.GetRolesAsync(customer)
                }
                );
        }


        [Authorize]
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            var user= await _userManager.GetUserAsync(User);
            if (user == null) return BadRequest("not found");
            await _userManager.UpdateSecurityStampAsync(user);
            return Ok("log Out succesfully");

        }
    }
}
