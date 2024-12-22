using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        //private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteModel(IEmployeeRepository employeeRepository)
        {
            //_context = context;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public Employees Employees { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return RedirectToPage();
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null)
            {
                return Page();
            }

            await _employeeRepository.DeleteEmployeeAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
