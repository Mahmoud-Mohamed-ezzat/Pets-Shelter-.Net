namespace Animal2.Dto.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; } 
        public string Email { get; set; }
        public IList<string> Role { get; set; }
        public string Token { get; set; }
    }
}
