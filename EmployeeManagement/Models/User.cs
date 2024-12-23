using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }  // This will be unique
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
