using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileUploadService _fileUploadService;
        public CreateModel(IEmployeeRepository employeeRepository, IFileUploadService fileUploadService)
        {
            _employeeRepository = employeeRepository;
            _fileUploadService = fileUploadService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]  
        public Employees Employees { get; set; } = default!;

        public string Message { get; set; }  // Property to hold the message


        public async Task<IActionResult> OnPostAsync()
        {
            Employees.DateOfBirth = Employees.DateOfBirth.ToUniversalTime();


            if (!ModelState.IsValid)
            {
                Message = "Invalid input. Please check your form.";  

                return Page();
            }

            try
            {
                int employeeId = await _employeeRepository.AddEmployeeAsync(Employees);
                Message = "Employee created successfully."; 
                return RedirectToPage("/Employee/Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return Page();
            }

        }

    }
}
