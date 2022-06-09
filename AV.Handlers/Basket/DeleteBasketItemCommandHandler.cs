using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Persistence.Queries;
using AV.Persistence.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Basket
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, BasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IBasketsQueries _basketsQueries;
        private readonly IStore<Common.Entities.Basket> _basketsStore;
        private readonly IUserManagerRepository _userManagerRepository;

        public DeleteBasketItemCommandHandler(
            IMapper mapper,
            IBasketsQueries basketsQueries,
            IStore<Common.Entities.Basket> basketsStore,
            IUserManagerRepository userManagerRepository)
        {
            _mapper = mapper;
            _basketsQueries = basketsQueries;
            _basketsStore = basketsStore;
            _userManagerRepository = userManagerRepository;
        }

        public async Task<BasketDto> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }

            // Initialize current user basket
            var usersCurrentBasket = await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken);

            var basketItem = usersCurrentBasket.Items.Find(b => b.Id == request.BasketItemId);
            if (basketItem == null)
            {
                throw new GoSmartValueException($"Not Basket Item with BasketItemId#:'{request.BasketItemId}' was found on current basket.");
            }

            usersCurrentBasket.Items.Remove(basketItem);

            usersCurrentBasket.RecalculateTotals();
            usersCurrentBasket.OrderNo = "dfdffd";

            await _basketsStore.Update(usersCurrentBasket, cancellationToken);
            return _mapper.Map<BasketDto>(await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken));
        }
    }
}