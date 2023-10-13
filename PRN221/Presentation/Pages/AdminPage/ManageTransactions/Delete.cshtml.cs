using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.AdminPage.ManageTransactions
{
    public class DeleteModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;

        public DeleteModel(IRentingTransactionRepository rentingTransactionRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
        }


        [BindProperty]
      public RentingTransaction RentingTransaction { get; set; } = default!;

        public  IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentingtransaction = _rentingTransactionRepository.GetByIdIncludes(id.Value);

            if (rentingtransaction == null)
            {
                return NotFound();
            }
            else 
            {
                RentingTransaction = rentingtransaction;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rentingtransaction =  _rentingTransactionRepository.GetById(id.Value);

            if (rentingtransaction != null)
            {
                RentingTransaction = rentingtransaction;
                _rentingTransactionRepository.Delete(RentingTransaction);
            }

            return RedirectToPage("./Index");
        }
    }
}
