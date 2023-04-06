using ASPNETMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVCCRUD.Models.Domain;
using ASPNETMVCCRUD.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ProductsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel addProductRequest)
        {
            var product = new Product() { 
                    ProductId = Guid.NewGuid(),
                    ProductName = addProductRequest.ProductName, 
                    ProductPrice = addProductRequest.ProductPrice,
                    ProductAvailability = addProductRequest.ProductAvailability
            };
            await mvcDemoDbContext.Products.AddAsync(product);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var products =   await mvcDemoDbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var product = await mvcDemoDbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                var viewModel = new UpdateProductViewModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductAvailability = product.ProductAvailability
                };
                return await Task.Run(() => View("view", viewModel));
            }

            return RedirectToAction("Index");

        }
        [HttpPost]

        public async Task<IActionResult> View(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.ProductId);
            if (product != null)
            {
                product.ProductId = model.ProductId;
                product.ProductName = model.ProductName;    
                product.ProductPrice = model.ProductPrice;
                product.ProductAvailability = model.ProductAvailability;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<IActionResult> Delete(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.ProductId);

            if (product != null)
            {
                mvcDemoDbContext.Products.Remove(product);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
