using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Orders.Queries.GetOrdersQuery;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersByCustomerIdQuery
{
    public class GetOrdersByCustomerIdQuery : IRequest<OrdersByCustomerIdVm>
    {
        public string UserId { get; set; }

        public class
            GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, OrdersByCustomerIdVm>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetOrdersByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OrdersByCustomerIdVm> Handle(GetOrdersByCustomerIdQuery request,
                CancellationToken cancellationToken)
            {
                var vm = new OrdersByCustomerIdVm();

                var orders = await _unitOfWork.Orders.GetAsync(
                    o => o.Customer.UserId == request.UserId,
                    o => o.OrderByDescending(x => x.OrderDate),
                    o => o
                        .Include(x => x.Customer)
                        .Include(x => x.OrderLines)
                        .ThenInclude(x => x.Product));

                vm.List = _mapper.Map<IList<OrdersByCustomerIdDto>>(orders);

                var orderList = orders.ToList();
                for (var i = 0; i < vm.List.Count; i++)
                    vm.List[i].OrderSummary = new ShoppingCartSummaryOrderByCustomerIdDto
                    {
                        TotalInclVat = orderList[i].OrderLines.GetTotalPrice(),
                        Vat = orderList[i].OrderLines.GetVat(),
                        TotalExVat = orderList[i].OrderLines.GetTotalPriceExclVat()
                    };

                return vm;
            }
        }
    }
}