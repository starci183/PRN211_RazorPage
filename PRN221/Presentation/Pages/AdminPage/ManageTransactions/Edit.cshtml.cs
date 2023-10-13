using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.AdminPage.ManageTransactions
{
    public class EditModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public EditModel(IRentingTransactionRepository rentingTransactionRepository, ICustomerRepository customerRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var rentingtransaction = _rentingTransactionRepository.GetByIdIncludes(id.Value);
            if (rentingtransaction == null)
            {
                return NotFound();
            }
            RentingTransaction = rentingtransaction;
           ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "CustomerId", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _rentingTransactionRepository.Update(RentingTransaction);
            return RedirectToPage("./Index");
        }
    }
}
