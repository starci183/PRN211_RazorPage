using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAOs.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Pages.AdminPage.ManageTransactions
{
    public class CreateModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public SelectList CustomerList { get; set; }
        public RentingTransactionValidation Errors = default!;
        public CreateModel(IRentingTransactionRepository rentingTransactionRepository, ICustomerRepository customerRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
            _customerRepository = customerRepository;

            CustomerList = new SelectList(_customerRepository.GetAll(), "CustomerId", "Email");

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
        public RentingTransaction RentingTransaction { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            Errors = new RentingTransactionValidation();
            Errors.Validate(RentingTransaction);
            if (Errors.HasError())
            {
                return Page();
            }

            _rentingTransactionRepository.Add(RentingTransaction);

            return RedirectToPage("./Index");
        }
    }
}
