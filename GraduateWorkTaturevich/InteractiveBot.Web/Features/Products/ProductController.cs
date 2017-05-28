using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Services;

namespace AimlBotWeb.Features.Products
{
    public class ProductController : Controller
    {
        private readonly IProductService _productSevice;

        public ProductController(IProductService productService)
        {
            _productSevice = productService;
        }

        // GET: products
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> All()
        {
            var products = await _productSevice.GetAll().ConfigureAwait(false);

            var productModels = products.Select(Mapper.Map<ProductModel>);

            return View(productModels);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Update(int id)
        {
            var product = await _productSevice.GetById(id).ConfigureAwait(false) ?? new Product();
            var model = Mapper.Map<ProductModel>(product);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductModel model)
        {
            var product = Mapper.Map<Product>(model);

            await _productSevice.Update(product).ConfigureAwait(false);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Details(int id)
        {
            var message = await _productSevice.GetById(id).ConfigureAwait(false) ?? new Product();
            var model = Mapper.Map<ProductModel>(message);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult Add()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ProductModel model)
        {
            var product = Mapper.Map<Product>(model);

            await _productSevice.Add(product).ConfigureAwait(false);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productSevice.GetById(id).ConfigureAwait(false);
            if (product != null)
            {
                await _productSevice.Delete(product);
            }

            return RedirectToAction(nameof(All));
        }
    }
}