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
    public class DeleteModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public DeleteModel(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [BindProperty]
      public CarInformation CarInformation { get; set; } = default!;

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

            var carInformation = _carRepository.GetByIdWithIncludes(id.Value);

            if (carInformation == null)
            {
                return NotFound();
            }
            else 
            {
                CarInformation = carInformation;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carInformation = _carRepository.GetById(id.Value);

            if (carInformation != null)
            {
                CarInformation = carInformation;
                _carRepository.Delete(CarInformation);
            }

            return RedirectToPage("./Index");
        }
    }
}
