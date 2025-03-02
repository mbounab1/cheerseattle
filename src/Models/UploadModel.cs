using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace todo.Models
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile File { get; set; }

        public string UploadResult { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (File == null || File.Length == 0)
            {
                UploadResult = "Please select a file to upload.";
                return Page();
            }

            var filePath = Path.Combine("wwwroot/uploads", File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            UploadResult = $"File '{File.FileName}' uploaded successfully!";
            return Page();
        }
    }
}