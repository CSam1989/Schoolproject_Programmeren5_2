using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Products.Queries.GetProductsByIdQuery
{
    public class GetProductByIdQuery : IRequest<ProductByIdVm>
    {
        public int? Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductByIdVm>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductByIdVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                if (request.Id is null)
                    throw new NotFoundException(nameof(Product), null);

                var vm = new ProductByIdVm();

                var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

                if (product is null)
                    throw new NotFoundException(nameof(Product), request.Id);

                vm.Product = _mapper.Map<ProductByIdDto>(product);

                return vm;
            }
        }
    }
}