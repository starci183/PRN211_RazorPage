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

        public void OnGet()
        {
            var customerJson = HttpContext.Session.GetString("account");
            if (customerJson == null) return;
            
            var deCustomer = JsonSerializer.Deserialize<Customer>(customerJson);
            if (deCustomer == null) return;

            Customer = deCustomer;

            Transactions = _rentingTransactionRepository.GetAllByCustomerId(Customer.CustomerId);
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        [BindProperty]
        public List<RentingTransaction> Transactions { get; set; } = default!;

    }
}
