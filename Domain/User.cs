namespace verticalSliceArchitecture.Domain
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken {  get; set; }
        public DateTime RefreshTokenExpiry{  get; set; }

    }
}
