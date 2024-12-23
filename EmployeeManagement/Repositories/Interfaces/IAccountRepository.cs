using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
