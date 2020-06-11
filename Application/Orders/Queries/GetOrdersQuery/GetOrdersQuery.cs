using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extentions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersQuery
{
    public class GetOrdersQuery : IRequest<OrdersVm>
    {
        public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrdersVm>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OrdersVm> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
            {
                var vm = new OrdersVm();

                var orders = await _unitOfWork.Orders
                    .GetAllAsync(null,
                        order => order
                            .Include(o => o.Customer)
                            .Include(o => o.OrderLines)
                            .ThenInclude(o => o.Product));

                vm.List = _mapper.Map<IList<OrdersDto>>(orders);

                var orderList = orders.ToList();

                for (var i = 0; i < vm.List.Count; i++)
                    vm.List[i].OrderSummary = new ShoppingCartSummaryDto
                    {
                        TotalCount = orderList[i].OrderLines.GetTotalItems(),
                        TotalInclVat = orderList[i].OrderLines.GetTotalPrice()
                    };

                return vm;
            }
        }
    }
}