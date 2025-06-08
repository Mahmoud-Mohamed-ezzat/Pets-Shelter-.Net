namespace Animal2.Dto.Requests
{
    public class AdminRequestsDto
    {
        public string AnimalName { get; set; }
        public string AnimalImage { get; set; }
        public string ShelterName { get; set; }
        public string AdopterName { get; set; }
        public string RequestStatus { get; set; }
        public DateOnly? InterviewDate { get; set; }
    }
}
