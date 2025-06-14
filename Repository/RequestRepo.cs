using Animal2.Dto.Requests;
using Animal2.Interface;
using Animal2.Mapper;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Repository
{
    public class RequestRepo : IRequest
    {
        private readonly AnimalsContext _context;
        public RequestRepo(AnimalsContext context)
        {
            _context = context;
        }

        public async Task<Request> AddRequest(AddRequestDto addRequestDto)
        {
            var request = addRequestDto.AddRequestDtoToRequest();
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<ShelterRequestsDto> ApproveRequest(int id, ApproveRequestDto approveRequestDto)
        {
            var request = await _context.Requests.Include(a => a.Animal).Include(a => a.Adopter).FirstOrDefaultAsync(a => a.Id == id);
            if (request == null)
            {
                return null;
            }
            request.Animal.AnimalStatus = "Not Available";
            request.Status = "Approved";
            request.InterviewDate = approveRequestDto.InterviewDate;
            await _context.SaveChangesAsync();
         return request.RequestToShelterRequestDto();
        }

        public async Task<List<AdminRequestsDto>> GetRequestsToAdmin()
        {
            var requests = await _context.Requests.Include(a => a.Animal).ThenInclude(b => b.ShelterStaff).Include(b => b.Adopter).ToListAsync();
            var AdminRequests = requests.Select(a => a.RequestToAdminRequestDto()).ToList();
            return AdminRequests;
        }

        public async Task<List<AdopterRequests>> GetRequestsToadopter(string adopterId)
        {
            var requests = await _context.Requests.Include(a => a.Adopter).Include(b => b.Animal).ThenInclude(c => c.ShelterStaff).ToListAsync();
            var adopterrequests = requests.Where(a => a.AdopterId == adopterId).ToList();
            var adopterRequests = adopterrequests.Select(a => a.ToAdopterRequests()).ToList();
            return adopterRequests;
        }

        public async Task<List<ShelterRequestsDto>> GetRequestsToShelter(string id)
        {
            var requests = await _context.Requests.Include(a => a.Adopter).Include(b => b.Animal).ToListAsync();
            var shelterRequests = requests.Where(a => a.Animal.ShelterStaffId == id && a.Status != "Rejected").ToList();
            var ShelterRequests = shelterRequests.Select(a => a.RequestToShelterRequestDto()).ToList();
            return ShelterRequests;
        }

        public async Task<ShelterRequestsDto> RejectRequest(int id)
        {
            var request = await _context.Requests.Include(a => a.Animal).ThenInclude(a => a.Adopter).FirstOrDefaultAsync(a => a.Id == id);
            if (request == null)
            {
                return null;
            }
            request.Status = "Rejected";
            await _context.SaveChangesAsync();
            return request.RequestToShelterRequestDto();
        }
    }
}
