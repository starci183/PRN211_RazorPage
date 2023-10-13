using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAOs.Models;

namespace Presentation.Pages.ManageCustomers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;


        public CustomerValidation Errors = default!;
        public CreateModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            Errors = new CustomerValidation();
            Errors.Validate(Customer);
            if (Errors.HasError())
            {
                return Page();
            }

            _customerRepository.Add(Customer);

            return RedirectToPage("./Index");
        }
    }
}
