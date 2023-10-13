using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DAOs.Models;
using System.Text.Json;

namespace Presentation.Pages.CustomerPage
{
    public class CustomerModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;

        public CustomerModel(IRentingTransactionRepository rentingTransactionRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
        }

        public IActionResult OnGet()
        {
            var customerJson = HttpContext.Session.GetString("account");
            if (customerJson == null) return RedirectToPage("/Index"); ;
            
            var deCustomer = JsonSerializer.Deserialize<Customer>(customerJson);
            if (deCustomer == null) return RedirectToPage("/Index"); ;

            Customer = deCustomer;

            Transactions = _rentingTransactionRepository.GetAllByCustomerId(Customer.CustomerId);

            return Page();
        }

        public IActionResult OnPostSignOut()
        {
            HttpContext.Session.Remove("account");
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        [BindProperty]
        public List<RentingTransaction> Transactions { get; set; } = default!;

    }
}
