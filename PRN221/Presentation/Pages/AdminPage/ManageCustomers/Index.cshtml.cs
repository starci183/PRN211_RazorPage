using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.ManageCustomers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("/Index");
            }

            Customer = _customerRepository.GetAll();
            return Page();
        }
    }
}
