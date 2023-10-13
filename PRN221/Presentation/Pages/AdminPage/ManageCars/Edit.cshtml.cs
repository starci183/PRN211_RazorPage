using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Pages.ManageCars
{
    public class EditModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplierRepository _supplierRepository;
        public SelectList ManufacturerSelectList { get; private set; }
        public SelectList SupplierSelectList { get; private set; }

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

            ManufacturerSelectList = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
            SupplierSelectList = new SelectList(_supplierRepository.GetAll(), "SupplierId", "SupplierName");
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

            var carInformation =  _carRepository.GetById(id.Value);
            if (carInformation == null)
            {
                return NotFound();
            }

            CarInformation = carInformation;

            return Page();
        }

        public CarValidation Errors { get; set; } = default!;
        public IActionResult OnPost()
        {
            Errors = new CarValidation();
            Errors.Validate(CarInformation);

            if (Errors.HasError())
            {
                return Page();
            }

            _carRepository.Update(CarInformation);
            return RedirectToPage("./Index");
        }
    }
}
