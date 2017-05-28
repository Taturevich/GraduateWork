using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> All()
        {
            var products = await _categoryService.GetAll();

            var categoryModels = products.Select(Mapper.Map<CategoryModel>);

            return View(categoryModels);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Update(int id)
        {
            var category = await _categoryService.GetById(id) ?? new Category();
            var model = Mapper.Map<CategoryModel>(category);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CategoryModel model)
        {
            var category = Mapper.Map<Category>(model);

            await _categoryService.Update(category);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryService.GetById(id) ?? new Category();
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
        public async Task<ActionResult> Add(CategoryModel model)
        {
            var category = Mapper.Map<Category>(model);

            await _categoryService.Add(category);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category != null)
            {
                await _categoryService.Delete(category);
            }

            return RedirectToAction(nameof(All));
        }
    }
}