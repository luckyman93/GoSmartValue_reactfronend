using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models.Payment.Commands;
using AV.Contracts.Models.Payment.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Payment
{
    public class RenewAccountSubscriptionCommandHandler : IRequestHandler<RenewAccountSubscriptionCommand, RenewAccountSubscriptionResponse>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IMapper _mapper;

        public RenewAccountSubscriptionCommandHandler(IPaymentsRepository paymentsRepository, IMapper mapper)
        {
            _paymentsRepository = paymentsRepository;
            _mapper = mapper;
        }

        public async Task<RenewAccountSubscriptionResponse> Handle(RenewAccountSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentsRepository.RenewAccountSubscription(request.AccountId);
            return new RenewAccountSubscriptionResponse
            {
                User = _mapper.Map<Contracts.Models.Users.UserModel>(user)
            };
        }
    }
}
