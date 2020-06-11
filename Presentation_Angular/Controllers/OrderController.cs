using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdatePaidInOrder;
using Application.Orders.Queries.GetOrderByIdQuery;
using Application.Orders.Queries.GetOrdersByCustomerIdQuery;
using Application.Orders.Queries.GetOrdersQuery;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly ICurrentUserService _currentUser;

        public OrderController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrdersVm>> Get()
        {
            return Ok(await Mediator.Send(new GetOrdersQuery()));
        }

        [HttpGet("PreviousOrders")]
        public async Task<ActionResult<OrdersByCustomerIdVm>> GetPreviousByCustomerId()
        {
            return Ok(await Mediator.Send(new GetOrdersByCustomerIdQuery
            {
                UserId = _currentUser.UserId
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderByIdVm>> GetById(int id)
        {
            //Hier wordt in de query nog gecontroleerd of current user gemachtigd is
            return Ok(await Mediator.Send(new GetOrderByIdQuery {Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, UpdatePaidInOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteOrderCommand {Id = id});

            return NoContent();
        }
    }
}