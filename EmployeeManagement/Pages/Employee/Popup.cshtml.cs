using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employee
{
    public class PopupModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Employee successfully added.";  // Set the message

        }
    }
}
