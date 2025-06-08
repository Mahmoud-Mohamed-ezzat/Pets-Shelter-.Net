using System.Runtime.CompilerServices;
using Animal2.Dto.messages;
using Animal2.Dto.Requests;
using Animal2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal2.Mapper
{
    public static class Requests
    {
        public static AdopterRequests ToAdopterRequests(this Request request)
        {
            return new AdopterRequests
            {
                Id = request.Id,
                AnimalName = request.Animal.AnimalName,
                AnimalImage = request.Animal.Image,
                RequestStatus = request.Status,
                ShelterName = request.Animal.ShelterStaff.UserName,
                ShelterAddress = request.Animal.ShelterStaff.ShelterAddress,
                InterviewDate = request.InterviewDate,
            };
        }
        public static Request AddRequestDtoToRequest(this AddRequestDto addrequestdto)
        {
            return new Request
            {
                AdopterId = addrequestdto.AdopterId,
                AnimalId = addrequestdto.AnimalId,
                Status = "Pending",
            };
        }
        public static AdminRequestsDto RequestToAdminRequestDto(this Request request)
        {
            return new AdminRequestsDto
            {
                AnimalName = request.Animal.AnimalName,
                AnimalImage = request.Animal.Image,
                ShelterName = request.Animal.ShelterStaff.UserName,
                AdopterName = request.Adopter.UserName,
                RequestStatus = request.Status,
                InterviewDate = request.InterviewDate,
            };
        }
        public static ShelterRequestsDto RequestToShelterRequestDto(this Request request)
        {
            return new ShelterRequestsDto
            {
                Id = request.Id,
                AnimalName = request.Animal.AnimalName,
                AnimalImage = request.Animal.Image,
                AdopterName = request.Adopter.UserName,
                RequestStatus = request.Status,
                InterviewDate = request.InterviewDate,
            };
        }
     
    }
}
