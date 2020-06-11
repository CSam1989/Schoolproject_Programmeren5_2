using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.ShoppingCart;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string StreetShipping { get; set; }
        public string HouseNrShipping { get; set; }
        public string HouseBusShipping { get; set; }
        public string PostalcodeShipping { get; set; }
        public string CityShipping { get; set; }
        public int CustomerId { get; set; }
        public List<CartItemDto> OrderLines { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = new Order
                {
                    IsPayed = false,
                    OrderDate = DateTime.Now,
                    StreetShipping = request.StreetShipping,
                    HouseNrShipping = request.HouseNrShipping,
                    HouseBusShipping = request.HouseBusShipping,
                    PostalcodeShipping = request.PostalcodeShipping,
                    CityShipping = request.CityShipping,
                    CustomerId = request.CustomerId
                };

                _unitOfWork.Orders.Create(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken); //

                foreach (var orderLine in request.OrderLines)
                {
                    var joinEntity = new OrderLine
                    {
                        OrderId = entity.Id,
                        ProductId = orderLine.Product.Id,
                        Quantity = orderLine.Quantity
                    };

                    _unitOfWork.OrderLines.Create(joinEntity);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}