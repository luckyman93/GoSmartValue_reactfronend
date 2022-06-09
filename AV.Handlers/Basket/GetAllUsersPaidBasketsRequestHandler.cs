using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Persistence.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Basket
{
    public class GetAllUsersPaidBasketsRequestHandler : IRequestHandler<GetAllUsersPaidBasketsRequest, IEnumerable<BasketDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBasketsQueries _basketsQueries;
        private readonly IUserManagerRepository _userManagerRepository;

        public GetAllUsersPaidBasketsRequestHandler(
            IMapper mapper,
            IBasketsQueries basketsQueries,
            IUserManagerRepository userManagerRepository)
        {
            _mapper = mapper;
            _basketsQueries = basketsQueries;
            _userManagerRepository = userManagerRepository;
        }

        public async Task<IEnumerable<BasketDto>> Handle(GetAllUsersPaidBasketsRequest request, CancellationToken cancellationToken)
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }
            
            return _mapper.Map<IEnumerable<BasketDto>>(await _basketsQueries.GetAllUsersPaidBaskets(user.Id));
        }
    }
}