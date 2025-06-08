namespace Animal2.Dto.Requests
{
    public class AdopterRequests
    {
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public string AnimalImage { get; set; }
        public string ShelterName { get; set; }
        public string ShelterAddress { get; set; }
        public string RequestStatus { get; set; }
        public DateOnly? InterviewDate { get; set; }

    }
}
