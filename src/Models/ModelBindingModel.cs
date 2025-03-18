using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;

namespace todo.Models
{
    public class ModelBindingModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine("here");
            Console.WriteLine($"{Request.Form.Keys}, information will be sent to ");
        }
    }
}