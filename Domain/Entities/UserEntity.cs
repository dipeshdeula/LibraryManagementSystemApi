namespace Domain.Entities
{
    public class UserEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string UserProfile { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Role { get;set; } = null!;

        public bool LoginStatus { get; set;} = false;



    }
}
