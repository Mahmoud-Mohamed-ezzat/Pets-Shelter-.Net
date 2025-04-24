using Animal2.Dto.Account;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Controllers
{


    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterStaffController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        public ShelterStaffController(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost("RegisterShelterstaff")]
        public async Task<IActionResult> RegisterShelterStaff([FromBody] ShelterStaffDto Sheltermodel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var Shelter = new Customer
                {
                    UserName = Sheltermodel.UserName,
                    Email = Sheltermodel.Email,
                    PhoneNumber = Sheltermodel.PhoneNumber,
                    ShelterAddress = Sheltermodel.ShelterAddress,
                    UserCategoryId = 3,
                };
                var CreatedAdopter = await _userManager.CreateAsync(Shelter, Sheltermodel.Password);
                /* .CreateAsync(Adopter,registermodel.Password);*/
                if (CreatedAdopter.Succeeded)
                {
                    var RoleResult = await _userManager.AddToRoleAsync(Shelter, "Shelterstaff");
                    if (RoleResult.Succeeded)
                    {
                        return Ok("created User Successfully");

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


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Shelterstaffs = await  _userManager.GetUsersInRoleAsync("Shelterstaff");
            var ShelterstaffDto = Shelterstaffs.Select(b => b.ToshelterstaffRetrieve()).ToList();
            return Ok(ShelterstaffDto);
        }

        [HttpGet("{id:string}")]
        public async Task<IActionResult> GetById(string Id)
        {
           var Shelterstaff=await _userManager.FindByIdAsync(Id);
            return Ok(Shelterstaff.ToshelterstaffRetrieve);
        }

         [HttpDelete("{Id:string}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var Shelterstaff = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(Shelterstaff);   
            return Ok("Delete successfully");
        }

        //[Authorize(Roles ="Admin")]
        [HttpPut("{Id:string}")]
        public async Task<IActionResult> Update(string Id, [FromBody] ShelterStaffDto shelterStaffDto)
        {
            if (!ModelState.IsValid) {return BadRequest(ModelState);}   
            var Shelterstaff = await _userManager.FindByIdAsync(Id);

            Shelterstaff.UserName=shelterStaffDto.UserName;
            Shelterstaff.PhoneNumber=shelterStaffDto.PhoneNumber;
            Shelterstaff.Email=shelterStaffDto.Email;
            await _userManager.UpdateAsync(Shelterstaff);   
            return Ok();
        }

    }
}