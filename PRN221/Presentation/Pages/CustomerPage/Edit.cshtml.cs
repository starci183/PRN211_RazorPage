using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;
using System.Text.Json;

namespace Presentation.Pages.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public EditModel(ICustomerRepository rentingTransactionRepository)
        {
            _customerRepository = rentingTransactionRepository;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public void OnGet()
        {
            var customerJson = HttpContext.Session.GetString("account");
            if (customerJson == null) return;

            var deCustomer = JsonSerializer.Deserialize<Customer>(customerJson);
            if (deCustomer == null) return;

            Customer = deCustomer;
        }

        public IActionResult OnPost()
        {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _customerRepository.Update(Customer);

                HttpContext.Session.SetString("account", JsonSerializer.Serialize(Customer));

                return RedirectToPage("./Index");
        }
    }
}
