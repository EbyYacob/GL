using System.ComponentModel.DataAnnotations;

namespace LQMSApplication.Model.User
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public string? CurrentJWT { get; set; }
        public string? DeviceID { get; set; }
        public string? UserType { get; set; }
    }
}
