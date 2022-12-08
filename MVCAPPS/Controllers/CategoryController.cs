using Microsoft.AspNetCore.Mvc;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess;
using Coditas.ECommerce.Repositories;
using MVCAPPS.CustomSessionExtension;
namespace MVCAPPS.Controllers
{
    public class CategoryController : Controller
    {
        IDbAccessService<Category, int> catRepo;

        public CategoryController(IDbAccessService<Category, int> catRepo)
        {
            this.catRepo = catRepo;
        }

        public async Task<IActionResult> Index()
        {
           /* try
            {*/
                var records = await catRepo.GetAsync();
                //This will return an Index.cshtml View from category Sub-folder of the views folder and pass the records(Categpries) to it.

                return View(records);
           /* }
            catch (Exception)
            {

                return View("Error");
            }*/
        }

        public async Task<IActionResult> IndexTagHelper() 
        {
            try
            {
                var records = await catRepo.GetAsync();
                return View(records);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public async Task<IActionResult> Create()
        {
            var Category = new Category();
            return View(Category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category) 
        {
            var response = await catRepo.CreateAsync(category);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id) 
        {
            try
            {
                var record = await catRepo.GetAsync(id);
                return View(record);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category) 
        {
            try
            {
                var result = await catRepo.UpdateAsync(id, category);
                return View(result);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int id) 
        {
            var result = await catRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        
    }
}