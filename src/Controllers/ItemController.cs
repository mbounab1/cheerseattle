namespace todo.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using todo.Models;

    using System;
    using System.IO;

    using System.Web;
    using System.Web.Mvc;

    using System.Web;

    using Microsoft.AspNetCore.Http;
    using static System.Net.Mime.MediaTypeNames;
    using Microsoft.Office.Interop.Excel;
    using Application = Microsoft.Office.Interop.Excel.Application;
    using System.Collections.Generic;
    using Microsoft.Azure.Cosmos.Linq;
    using System.Web.WebPages;

    public class ItemController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ItemController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c ORDER BY c.Name Asc"));
        }

        [ActionName("Attendance")]
        public IActionResult Attendance(Attendance attendanceRecord)
        {
            return View(attendanceRecord);
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IFormFile file)
        {
            try
            {
                // Handle file upload logic here
                if (file != null && file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string[] allowedExtensions = { ".xlsx", ".csv" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        var maxFileSize = 5 * 1024 * 1024; // 5MB
                        if (file.Length > maxFileSize)
                        {
                            ViewBag.Message = "File size exceeds the maximum limit of 5MB.";
                            return View();
                        }

                        ProcessFile(file);

                        // Display success message
                        ViewBag.Message = "File uploaded successfully!";
                    }
                    else
                    {
                        ViewBag.Message = "Only pdf, doc and docx files are allowed.";
                    }
                }
                else
                {
                    // Display error message
                    ViewBag.Message = "Please select a file to upload.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Item item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Item item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }

        private void ProcessFile(IFormFile file)
        {
            //read file
            var attendanceRecord = new Dictionary<(string, string), string>();
            try
            {
                using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        var rsvpInfo = line.Split(",");
                        var status = rsvpInfo[0];
                        var name = rsvpInfo[1].Split(" ");
                        var firstName = name[0];
                        var lastName = name[1];

                        Console.WriteLine(status + " " + firstName + " " + lastName);

                        attendanceRecord.Add((firstName, lastName), status);
                    }
                    Record(attendanceRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void Record(Dictionary<(string, string), string> rsvpRecord)
        {
            var today = DateTimeOffset.Now.Date.ToString();
            var allVolunteers = await _cosmosDbService.GetItemsAsync("SELECT * FROM c ORDER BY c.Name Asc");
            foreach (var volunteer in allVolunteers)
            {
                var status = rsvpRecord.GetValueOrDefault((volunteer.FirstName, volunteer.LastName));
                if (string.IsNullOrWhiteSpace(status))
                {
                    volunteer.Attendance.Unexcused.Add(today);
                }
                else if (status == RsvpStatus.Join)
                {
                    volunteer.Attendance.Present.Add(today);
                }
                else if (status == RsvpStatus.Decline)
                {
                    volunteer.Attendance.Absent.Add(today);
                }
                else if (status == RsvpStatus.Late)
                {
                    volunteer.Attendance.Late.Add(today);
                }

                try
                {
                    Console.WriteLine(volunteer.Name);
                    await _cosmosDbService.UpdateItemAsync(volunteer.Name.Trim(), volunteer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}