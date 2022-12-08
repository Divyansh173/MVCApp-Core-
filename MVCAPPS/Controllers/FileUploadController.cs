using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;


namespace MVCAPPS.Controllers
{
    public class FileUploadController : Controller
    {
        IWebHostEnvironment hostEnvironment;

        public FileUploadController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index() 
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(IFormFile file) 
        {
            try
            {
                if (file == null)
                    throw new Exception("File is not received ....");
                string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedFile");
                if (!Directory.Exists(dirPath)) 
                {
                    Directory.CreateDirectory(dirPath);
                }

                string dataFilename = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(dataFilename);
                string[] allowedextension = new string[] { ".png", ".jpeg" };
                if (!allowedextension.Contains(extension))
                    throw new Exception("Sorry this file extension is not allowed");
                string saveTopath = Path.Combine(dirPath,dataFilename);

                using (FileStream stream = new FileStream(saveTopath, FileMode.Create)) 
                {
                    await file.CopyToAsync(stream);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
