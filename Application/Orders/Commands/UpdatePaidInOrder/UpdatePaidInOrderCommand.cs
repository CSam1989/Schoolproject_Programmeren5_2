using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.UpdatePaidInOrder
{
    public class UpdatePaidInOrderCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsPayed { get; set; }

        public class UpdatePaidInOrderCommandHandler : IRequestHandler<UpdatePaidInOrderCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdatePaidInOrderCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdatePaidInOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.Orders.GetByIdAsync(request.Id);

                if (entity is null)
                    throw new NotFoundException(nameof(Order), request.Id);

                entity.IsPayed = request.IsPayed;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}