using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear the authentication cookie
            Response.Cookies.Delete("AuthToken");

            // Redirect to the login page after logout
            return RedirectToPage("/Account/Login");
        }
    }
}
