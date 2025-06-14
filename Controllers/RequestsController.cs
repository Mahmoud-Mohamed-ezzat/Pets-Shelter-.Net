using Animal2.Dto.animal;
using Animal2.Dto.Requests;
using Animal2.Mapper;
using Animal2.Models;
using Animal2.Repository;
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
        private readonly RequestRepo _requestRepo;
        public RequestsController(AnimalsContext context, RequestRepo requestRepo)
        {
            _context = context;
            _requestRepo = requestRepo;
        }

        [Authorize(Roles = "Adopter")]
        [HttpGet("Adopter/{adopterId}")] //authorized by adopter
        public async Task<IActionResult> GetRequestsToadopter(string adopterId)
        {
            var adopterRequests = await _requestRepo.GetRequestsToadopter(adopterId);
            return Ok(adopterRequests);
        }
        [Authorize(Roles = "Adopter")]
        [HttpPost]
        public async Task<IActionResult> AddRequest([FromBody] AddRequestDto addRequestDto) //authorized by adopter
        {
            var request = await _requestRepo.AddRequest(addRequestDto);
            return Ok(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRequestsToAdmin() //authorized by admin his  id it's static in DB to be secure not passed it to API 
        {
            var AdminRequests = await _requestRepo.GetRequestsToAdmin();
            return Ok(AdminRequests);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpGet("Shelter/{id}")]
        public async Task<IActionResult> GetRequestsToShelter(string id) //authorized by shelter staff
        {
            var requests = await _requestRepo.GetRequestsToShelter(id);
            return Ok(requests);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveRequest(int id, [FromBody] ApproveRequestDto approveRequestDto) //authorized by shelter staff
        {
            var request = await _requestRepo.ApproveRequest(id, approveRequestDto);
            return Ok(request);
        }

        [Authorize(Roles = "Shelterstaff")]
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectRequest(int id) //authorized by shelter staff
        {
            var request = await _requestRepo.RejectRequest(id);
            return Ok(request);
        }
    }
}