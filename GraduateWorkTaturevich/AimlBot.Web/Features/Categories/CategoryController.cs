using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Services;

namespace AimlBotWeb.Features.Categories
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: products
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult All()
        {
            var products = _categoryService.GetAll();

            var categoryModels = products.Select(Mapper.Map<CategoryModel>);

            return View(categoryModels);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Update(int id)
        {
            var category = _categoryService.GetById(id) ?? new Category();
            var model = Mapper.Map<CategoryModel>(category);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(CategoryModel model)
        {
            var category = Mapper.Map<Category>(model);

            _categoryService.Update(category);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Details(int id)
        {
            var category = _categoryService.GetById(id) ?? new Category();
            var model = Mapper.Map<CategoryModel>(category);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Add()
        {
            return View(new CategoryModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryModel model)
        {
            var category = Mapper.Map<Category>(model);

            _categoryService.Add(category);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category != null)
            {
                _categoryService.Delete(category);
            }

            return RedirectToAction(nameof(All));
        }
    }
}