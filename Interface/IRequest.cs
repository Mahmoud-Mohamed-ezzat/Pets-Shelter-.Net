using Animal2.Dto.Requests;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Interface
{
    public interface IRequest
    {
        Task<List<AdopterRequests>> GetRequestsToadopter(string adopterId);
        Task<List<AdminRequestsDto>> GetRequestsToAdmin();
        Task<Request> AddRequest(AddRequestDto addRequestDto);
        Task<List<ShelterRequestsDto>> GetRequestsToShelter(string id);
        Task<ShelterRequestsDto> ApproveRequest(int id, ApproveRequestDto approveRequestDto);
        Task<ShelterRequestsDto> RejectRequest(int id);
    }
}
