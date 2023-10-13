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
    public class DetailsModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public DetailsModel(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public CarInformation CarInformation { get; set; } = default!; 

        public IActionResult OnGet(int? id)
        {
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
    }
}
