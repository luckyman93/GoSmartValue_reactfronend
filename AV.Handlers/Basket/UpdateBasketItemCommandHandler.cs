using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
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
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommand, BasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Common.Entities.Product> _productsRepository;
        private readonly IBasketsQueries _basketsQueries;
        private readonly IStore<Common.Entities.Basket> _basketsStore;
        private readonly IUserManagerRepository _userManagerRepository;

        public UpdateBasketItemCommandHandler(
            IMapper mapper,
            IRepository<Common.Entities.Product> productsRepository,
            IBasketsQueries basketsQueries,
            IStore<Common.Entities.Basket> basketsStore,
            IUserManagerRepository userManagerRepository)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
            _basketsQueries = basketsQueries;
            _basketsStore = basketsStore;
            _userManagerRepository = userManagerRepository;
        }

        public async Task<BasketDto> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId ?? default);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }
            // get product
            var product = _productsRepository.Get(request.ProductId);
            if (product == null)
            {
                throw new GoSmartValueException($"Not Product with ProductId#:'{request.ProductId}' was found.");
            }

            // Initialize current user basket
            var usersCurrentBasket = await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken);

            var basketItem = usersCurrentBasket.Items.Find(b => b.Id == request.Id);
            if (basketItem == null)
            {
                throw new GoSmartValueException($"Not Basket Item with BasketItemId#:'{request.Id}' was found on current basket.");
            }

            basketItem.ProductId = product.ProductId;
            basketItem.UnitPrice = product.Price ?? 0;
            basketItem.Quantity = request.Quantity;
            basketItem.PromoCode = request.PromoCode;
            basketItem.InputData = _mapper.Map<BasketItemInputData>(request.InputData);
            basketItem.Price = (product.Price.Value * basketItem.Quantity) * (1 - basketItem.Discount / 100);

            basketItem.Update(request.UserId.Value);

            usersCurrentBasket.RecalculateTotals();

            await _basketsStore.Update(usersCurrentBasket, cancellationToken);
            return _mapper.Map<BasketDto>(await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken));
        }
    }
}