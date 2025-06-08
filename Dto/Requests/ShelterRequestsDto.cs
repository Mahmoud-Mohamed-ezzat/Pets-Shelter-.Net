namespace Animal2.Dto.Requests
{
    public class ShelterRequestsDto
    {
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public string AnimalImage { get; set; }
        public string AdopterName { get; set; }
        public string RequestStatus { get; set; }
        public DateOnly? InterviewDate { get; set; }
    }
}