using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using Upload_File.Interface;
using Upload_File.Interface.Implementation;
using Upload_File.Models;

namespace Upload_File.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUploadFiles uploadFiles;

        public HomeController(ILogger<HomeController> logger, IUploadFiles uploadFiles)
        {
            _logger = logger;
            this.uploadFiles = uploadFiles;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UploadFileBuffered()
        {
            return View();
        }

        public IActionResult UploadFileStream()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // The name of the function need to be the same with
        // the action above
        [HttpPost("Home/UploadFileBuffered")]
        public async Task<ActionResult> UploadFileBuffered(IFormFile file)
        {
            try
            {
                if (await uploadFiles.UploadFileBuffered(file))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

        // You can change the function name as you like
        // but you need to define the ActionName first
        // above the function
        [ActionName("UploadFileStream")]
        [HttpPost("Home/UploadFileStream")]
        public async Task<IActionResult> SaveFileToPhysicalFolder()
        {
            var boundary = HeaderUtilities.RemoveQuotes(
             MediaTypeHeaderValue.Parse(Request.ContentType).Boundary
            ).Value;

            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();

            string response = string.Empty;
            try
            {
                if (await uploadFiles.UploadFileStream(reader, section))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }
    }
}