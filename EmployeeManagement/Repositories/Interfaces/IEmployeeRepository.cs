using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employees>> GetAllEmployeesAsync();
        Task<Employees?> GetEmployeeByIdAsync(int? employeeId);
        Task<int> AddEmployeeAsync(Employees employee);
        Task UpdateEmployeeAsync(Employees employee);
        Task DeleteEmployeeAsync(int? employeeId);
    }
}
