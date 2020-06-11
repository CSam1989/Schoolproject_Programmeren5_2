using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Customers.Queries.GetCustomerByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICurrentUserService _currentUser;

        public CustomerController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerByUserIdVm>> GetByUserId()
        {
            return Ok(await Mediator.Send(new GetCustomerByUserIdQuery {UserId = _currentUser.UserId}));
        }
    }
}