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

        public void OnGet()
        {
              RentingTransaction = _rentingTransactionRepository.GetAllIncludes();
        }
    }
}
