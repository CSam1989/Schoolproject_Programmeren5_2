using System.Threading.Tasks;
using Application.Products.Commands.CreateProductCommand;
using Application.Products.Commands.DeleteProductCommand;
using Application.Products.Commands.UpdateProductCommand;
using Application.Products.Queries.GetProductsByIdQuery;
using Application.Products.Queries.GetProductsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Angular.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductsVm>> Get()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductByIdVm>> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery {Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new {id = productId}, command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductCommand {Id = id});

            return NoContent();
        }
    }
}