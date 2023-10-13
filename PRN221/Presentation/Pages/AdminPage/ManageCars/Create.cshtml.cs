using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAOs.Models;

namespace Presentation.Pages.ManageCars
{
    public class CreateModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplierRepository _supplierRepository;

        public CreateModel(
            ICarRepository carRepository,
            IManufacturerRepository manufacturerRepository,
            ISupplierRepository supplierRepository
            )
        {
            _carRepository = carRepository;
            _manufacturerRepository = manufacturerRepository;
            _supplierRepository = supplierRepository;
        }


        public IActionResult OnGet()
        {
        ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(_supplierRepository.GetAll(), "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          //if (!ModelState.IsValid ||  CarInformation == null)
          //  {
          //      return Page();
          //  }

            _carRepository.Add(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
