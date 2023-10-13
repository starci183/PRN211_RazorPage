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

        public void OnGet()
        {
            if (_carRepository.GetAll() != null)
            {
                CarInformation = _carRepository.GetCarsWithIncludes();
            }
        }
    }
}
