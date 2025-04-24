using Animal2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        private readonly AnimalsContext _context;
        public RequestsController(AnimalsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _context.Requests.ToListAsync();
            return Ok(requests);
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == Id);
            return Ok(request);
        }
    }
}
