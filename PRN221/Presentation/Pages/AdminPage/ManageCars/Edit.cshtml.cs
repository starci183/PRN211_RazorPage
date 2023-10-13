using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.ManageCars
{
    public class EditModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplierRepository _supplierRepository;

        [ActivatorUtilitiesConstructor]
        public EditModel(
            ICarRepository carRepository,
            IManufacturerRepository manufacturerRepository,
            ISupplierRepository supplierRepository
            )
        {
            _carRepository = carRepository;
            _manufacturerRepository = manufacturerRepository;
            _supplierRepository = supplierRepository;
        }

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null || _carRepository.GetAll() == null)
            {
                return NotFound();
            }

            var carInformation =  _carRepository.GetById(id.Value);
            if (carInformation == null)
            {
                return NotFound();
            }
            CarInformation = carInformation;

           ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
           ViewData["SupplierId"] = new SelectList(_supplierRepository.GetAll(), "SupplierId", "SupplierName");
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

            _carRepository.Update(CarInformation);
            return RedirectToPage("./Index");
        }
    }
}
