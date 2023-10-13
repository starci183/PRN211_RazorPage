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
    public class IndexModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;

        public IndexModel(IRentingTransactionRepository rentingTransactionRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
        }

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("/Index");
            }
            RentingTransaction = _rentingTransactionRepository.GetAllIncludes();
            return Page();
        }
    }
}
