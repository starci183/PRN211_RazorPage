using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.ManageCars
{
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public IndexModel(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IList<CarInformation> CarInformation { get;set; } = default!;

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("/Index");
            }

            if (_carRepository.GetAll() != null)
            {
                CarInformation = _carRepository.GetCarsWithIncludes();
            }
            return Page();
        }
    }
}
