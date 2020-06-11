using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Customers.Queries.GetCustomerByUserId
{
    public class GetCustomerByUserIdQuery : IRequest<CustomerByUserIdVm>
    {
        public string UserId { get; set; }

        public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, CustomerByUserIdVm>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public GetCustomerByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CustomerByUserIdVm> Handle(GetCustomerByUserIdQuery request,
                CancellationToken cancellationToken)
            {
                var vm = new CustomerByUserIdVm();

                var customer = await _unitOfWork.Customers.GetFirstAsync(c => c.UserId == request.UserId);

                vm.Customer = _mapper.Map<CustomerByUserIdDto>(customer);

                return vm;
            }
        }
    }
}