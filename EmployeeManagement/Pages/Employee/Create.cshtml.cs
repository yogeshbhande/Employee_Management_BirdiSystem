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

        [BindProperty]
        public IFormFile File { get; set; }

        public string FileUploadError { get; set; }

        public string Message { get; set; }  // Property to hold the message


        public async Task<IActionResult> OnPostAsync()
        {
            Employees.DateOfBirth = Employees.DateOfBirth.ToUniversalTime();


            // First, check if the file is available and has a valid content type
            if (File == null || File.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please upload a file.");
                return Page();
            }

            if (File.ContentType != "application/pdf")
            {
                ModelState.AddModelError(string.Empty, "Only PDF files are allowed.");
                Message = "Only PDF files are allowed."; 
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Message = "Invalid input. Please check your form.";  

                return Page();
            }

            try
            {
                int employeeId = await _employeeRepository.AddEmployeeAsync(Employees);

                // Save the file and create the file mapping
                string filePath = await _fileUploadService.SaveFileAsync(File, employeeId);

                var fileMapping = new FileUploadEmployeeMapping
                {
                    EmployeeId = employeeId,
                    FileName = File.FileName,
                    FilePath = filePath,
                    UploadDate = DateTime.UtcNow,
                    FileType = File.ContentType
                };

                // Insert the file mapping into the database
                await _employeeRepository.AddFileUploadEmployeeMappingAsync(fileMapping);

                Message = "Employee created successfully."; // Success message
                return RedirectToPage("/Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return Page();
            }

        }

    }
}
