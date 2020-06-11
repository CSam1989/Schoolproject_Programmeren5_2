using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Products.Queries.GetProductsQuery
{
    public class GetProductsQuery : IRequest<ProductsVm>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var vm = new ProductsVm();


                var products = await _unitOfWork.Products
                    .GetAllAsync();

                vm.List = _mapper.Map<IList<ProductListDto>>(products);

                return vm;
            }
        }
    }
}