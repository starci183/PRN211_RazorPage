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
        
        public CustomerValidation Errors = default!;
        public SelectList CustomerList { get; set; }

        public EditModel(ICustomerRepository rentingTransactionRepository)
        {
            _customerRepository = rentingTransactionRepository;
            CustomerList = new SelectList(_customerRepository.GetAll(), "CustomerId", "Email");
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public IActionResult OnGet()
        {   
            var customerJson = HttpContext.Session.GetString("account");
            if (customerJson == null) return RedirectToPage("/Index");

            var deCustomer = JsonSerializer.Deserialize<Customer>(customerJson);
            if (deCustomer == null) return NotFound();

            Customer = deCustomer;
            return Page();
        }

        public IActionResult OnPost()
        {

            Errors = new CustomerValidation();
            Errors.Validate(Customer);
            if (Errors.HasError())
            {
                return Page();
            }

            _customerRepository.Update(Customer);

                HttpContext.Session.SetString("account", JsonSerializer.Serialize(Customer));

                return RedirectToPage("./Index");
        }
    }
}
