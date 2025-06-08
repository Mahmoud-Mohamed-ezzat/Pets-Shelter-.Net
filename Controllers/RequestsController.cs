using Animal2.Dto.animal;
using Animal2.Dto.Requests;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Controllers
{
    [Route("api/Requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        private readonly AnimalsContext _context;
        public RequestsController(AnimalsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Adopter")]
        [HttpGet("Adopter/{adopterId}")] //authorized by adopter
        public async Task<IActionResult> GetRequestsToadopter(string adopterId)
        {
            var requests = await _context.Requests.Include(a => a.Adopter).Include(b => b.Animal).ThenInclude(c => c.ShelterStaff).ToListAsync();
            var adopterrequests = requests.Where(a => a.AdopterId == adopterId).ToList(); 
            var adopterRequests = adopterrequests.Select(a => a.ToAdopterRequests());
            return Ok(adopterRequests);
        }

        [Authorize(Roles = "Adopter")]
        [HttpPost]
        public async Task<IActionResult> AddRequest([FromBody] AddRequestDto addRequestDto) //authorized by adopter
        {
            var request = addRequestDto.AddRequestDtoToRequest();
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRequestsToAdmin() //authorized by admin
        {
            var requests = await _context.Requests.Include(a => a.Animal).ThenInclude(b => b.ShelterStaff).Include(b => b.Adopter).ToListAsync();
            var AdminRequests = requests.Select(a => a.RequestToAdminRequestDto()).ToList();
            return Ok(AdminRequests);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpGet("Shelter/{id}")]
        public async Task<IActionResult> GetRequestsToShelter(string id) //authorized by shelter staff
        {
            var requests = await _context.Requests.Include(a => a.Adopter).Include(b => b.Animal).ToListAsync();
            var shelterRequests = requests.Where(a => a.Animal.ShelterStaffId == id && a.Status != "Rejected").ToList();
            var ShelterRequests = shelterRequests.Select(a => a.RequestToShelterRequestDto());
            return Ok(ShelterRequests);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveRequest(int id, [FromBody] ApproveRequestDto approveRequestDto) //authorized by shelter staff
        {
            var request = await _context.Requests.Include(a => a.Animal).Include(a=>a.Adopter).FirstOrDefaultAsync(a => a.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            request.Animal.AnimalStatus = "Not Available";
            request.Status = "Approved";
            request.InterviewDate = approveRequestDto.InterviewDate;
            await _context.SaveChangesAsync();
            return Ok(request.RequestToShelterRequestDto());
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectRequest(int id) //authorized by shelter staff
        {
            var request = await _context.Requests.Include(a => a.Animal).ThenInclude(a => a.Adopter).FirstOrDefaultAsync(a => a.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "Rejected";
            await _context.SaveChangesAsync();
            return Ok(request.RequestToShelterRequestDto());
        }

    }
}