using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtTokenService _jwtTokenService;
        private readonly PasswordHasher<User> _passwordHasher;
        public LoginModel(IAccountRepository accountRepository, JwtTokenService jwtTokenService)
        {
            _accountRepository = accountRepository;
            _jwtTokenService = jwtTokenService; 
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _accountRepository.GetUserByEmailAsync(Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            var hashedPassword = HashPassword(Password);

            // Compare the manually hashed password with the stored hash from the database
            if (user.PasswordHash != hashedPassword)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // Step 3: Generate the JWT token if the credentials are correct
            var token = _jwtTokenService.GenerateToken(user);

            Response.Cookies.Append("AuthToken", token, new CookieOptions { HttpOnly = true });

            return RedirectToPage("/Employee/Index");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
