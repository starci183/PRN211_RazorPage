using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Pages.ManageCustomers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
       
        public CustomerValidation Errors = default!;

        public EditModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("/Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var customer =  _customerRepository.GetById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
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
       
            return RedirectToPage("./Index");
        }

    }
}
