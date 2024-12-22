using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public EditModel(AppDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public Employees Employees { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return RedirectToPage("./Index");
            }

            var employees = await _employeeRepository.GetEmployeeByIdAsync(id); 

            if(employees == null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Employees = employees;
                return Page();
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _employeeRepository.UpdateEmployeeAsync(Employees);
            return RedirectToPage();
        }
    }
}
