using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAOs.Models;

namespace Presentation.Pages.AdminPage.ManageTransactions
{
    public class CreateModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateModel(IRentingTransactionRepository rentingTransactionRepository, ICustomerRepository customerRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "CustomerId", "Email");
            return Page();
        }

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _rentingTransactionRepository.Add(RentingTransaction);

            return RedirectToPage("./Index");
        }
    }
}
