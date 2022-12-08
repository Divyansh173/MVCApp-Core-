using Microsoft.AspNetCore.Mvc;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess;
using Coditas.ECommerce.Repositories;
using MVCAPPS.CustomSessionExtension;

namespace MVCAPPS.Controllers
{
    public class SubcategoryController : Controller
    {
        IDbAccessService<SubCategory, int> SubcatRepo;

        public SubcategoryController(IDbAccessService<SubCategory, int> SubcatRepo)
        {
            this.SubcatRepo = SubcatRepo;
        }

        public async Task<IActionResult> Index()
        {
            var records = await SubcatRepo.GetAsync();
                //This will return an Index.cshtml View from category Sub-folder of the views folder and pass the records(Categpries) to it.
            return View(records);
        }

        public async Task<IActionResult> Create()
        {
            var SubCategory = new SubCategory();
            return View(SubCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubCategory category)
        {
            var response = await SubcatRepo.CreateAsync(category);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
                var record = await SubcatRepo.GetAsync(id);
                return View(record);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SubCategory category)
        {
            try
            {
                var result = await SubcatRepo.UpdateAsync(id, category);
                return View(result);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await SubcatRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }

}
