using System.Threading.Tasks;
using Application.Products.Commands.CreateProductCommand;
using Application.Products.Commands.DeleteProductCommand;
using Application.Products.Commands.UpdateProductCommand;
using Application.Products.Queries.GetProductsByIdQuery;
using Application.Products.Queries.GetProductsQuery;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_MVC.Controllers
{
    // Dit is het beheer van producten, dus enkel voor admin!!
    // De productlijst voor bezoekers gaat via de HomeController
    [Authorize(Roles = "Admin")]
    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? currentPage, string sortBy, string currentFilter,
            string searchString)
        {
            return View(await Mediator.Send(new GetProductsQuery
            {
                CurrentPage = currentPage,
                SortBy = sortBy,
                SearchString = searchString,
                CurrentFilter = currentFilter
            }));
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Category,Description,PhotoUrl")]
            CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var productId = await Mediator.Send(command);
                return RedirectToAction(nameof(Index), productId);
            }

            return View(command);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var e = await Mediator.Send(new GetProductByIdQuery {Id = id});

            // Dit is geen mooie solution, maar Ik zag geen andere oplossing..
            // Bij het Oproepen van deze methode word de productByIdDto gebruikt
            // Bij het eigenlijke updaten van de entity wordt de class UpdateCommand gebruikt
            var product = _mapper.Map<UpdateProductCommand>(e.Product);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,Description,PhotoUrl")]
            UpdateProductCommand command)
        {
            if (id != command.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await Mediator.Send(new GetProductByIdQuery {Id = id}));
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteProductCommand {Id = id});

            return RedirectToAction(nameof(Index));
        }
    }
}