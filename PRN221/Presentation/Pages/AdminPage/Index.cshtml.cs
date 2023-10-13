using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System.ComponentModel;

namespace Presentation.Pages.AdminModel
{
    public class AdminModel : PageModel
    {
        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (!isAdmin.HasValue)
            {
                return RedirectToPage("");
            }
            return Page();
        }

            public static void OnPostCreate()
            {
                // Set the license context for EPPlus
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Get the user's "Documents" folder path
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Specify the file path where you want to save the Excel report in the "Documents" folder
                string filePath = Path.Combine(documentsFolderPath, "ExcelReport.xlsx");

                // Create a new Excel package
                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the Excel package
                    var worksheet = package.Workbook.Worksheets.Add("Report");


                    // Save the Excel package to the specified file path
                    package.SaveAs(new FileInfo(filePath));

                    Console.WriteLine($"Excel report generated and saved to {filePath}");
                }
            }
        public IActionResult OnPostSignOut()
        {
            HttpContext.Session.Remove("isAdmin");
            return RedirectToPage("/Index");
        }
    }


}
