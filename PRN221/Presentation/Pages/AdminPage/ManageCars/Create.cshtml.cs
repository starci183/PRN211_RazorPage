using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAOs.Models;
using System.Text.Json;

namespace Presentation.Pages.ManageCars
{
    public class CreateModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplierRepository _supplierRepository;

        public SelectList ManufacturerSelectList { get; private set; }
        public SelectList SupplierSelectList { get; private set; }

        public CreateModel(
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

        public CarValidation Errors { get; set; } = default!;

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;

        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue) {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Errors = new CarValidation();
            Errors.Validate(CarInformation);

            Console.WriteLine(JsonSerializer.Serialize(Errors));
            if (Errors.HasError())
            {
                return Page();
            }
            _carRepository.Add(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
