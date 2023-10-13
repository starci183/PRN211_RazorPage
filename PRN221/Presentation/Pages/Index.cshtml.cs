using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Presentation.Pages
{
    public class IndexModel : PageModel
    {

        public string? ErrorMessage;

        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            ErrorMessage = null;
        }

  
        [BindProperty]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("isAdmin").HasValue)
            {
                return Redirect("/AdminPage");
            }
            else if (HttpContext.Session.GetString("account") != null)
            {
                return Redirect("/CustomerPage");
            }
            else return Page();
        }


        public IActionResult OnPostSignIn()
        {   
            if (CheckAdmin()){
                HttpContext.Session.SetInt32("isAdmin", 1);
                return Redirect("/AdminPage");
            }
            var account = _customerRepository.GetByEmailAndPassword(Email, Password);
            
            if (account == null)
            {
                ErrorMessage = "Incorrect email or password. Please try again.";
                return Page();
            }

            HttpContext.Session.SetString("account", JsonSerializer.Serialize(account));

            Console.WriteLine(account.CustomerBirthday);

            return Redirect("/CustomerPage");
        }


        private bool CheckAdmin()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var email = config["Admin:Email"];
            var password = config["Admin:Password"];

            return Email == email && Password == password;

        }
    }
}
