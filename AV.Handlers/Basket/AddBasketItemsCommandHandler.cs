using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Persistence.Queries;
using AV.Persistence.Stores;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Interfaces.Services;
using AV.Contracts.Models.Users;
using Microsoft.Extensions.Options;

namespace AV.Handlers.Basket
{
    public class AddBasketItemsCommandHandler : IRequestHandler<AddBasketItemsCommand, BasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Common.Entities.Product> _productsRepository;
        private readonly IBasketsQueries _basketsQueries;
        private readonly IStore<Common.Entities.Basket> _basketsStore;
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IEmailService _emailService;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;

        public AddBasketItemsCommandHandler(
            IMapper mapper,
            IRepository<Common.Entities.Product> productsRepository,
            IBasketsQueries basketsQueries,
            IStore<Common.Entities.Basket> basketsStore,
            IUserManagerRepository userManagerRepository,
            IEmailService emailService,
            IOptions<SmtpConfiguration> smtpOptions)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
            _basketsQueries = basketsQueries;
            _basketsStore = basketsStore;
            _userManagerRepository = userManagerRepository;
            _emailService = emailService;
            _smtpOptions = smtpOptions;
        }

        public async Task<BasketDto> Handle(AddBasketItemsCommand request, CancellationToken cancellationToken)
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }
            // get product
            var productIds = request.Items.Select(i => i.ProductId).Distinct();
            var products = await _productsRepository.GetAll()
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync(cancellationToken);
            if (products == null || products.Count() != productIds.Count())
            {
                throw new GoSmartValueException($"Not all Products with ids#:'{productIds}' were found.");

            }

            // Initialize current user basket
            var usersCurrentBasket = await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken);

            var items = ConvertToBasketItems(request.Items);
            foreach (var basketItem in items)
            {
                var product = products.Find(p => p.ProductId == basketItem.ProductId);
                basketItem.Product = product;
                basketItem.ProductId = product.ProductId;
                basketItem.ProductName = product.Name;
                basketItem.UnitPrice = product.Price.Value;
                basketItem.Price = (product.Price.Value * basketItem.Quantity) * (1 - basketItem.Discount / 100);
                
                if (basketItem.InputData.LocalityId == 0) basketItem.InputData.LocalityId = null;
                if (basketItem.InputData.StreetId == 0) basketItem.InputData.StreetId = null;

                if (basketItem.Id == default)
                {
                    usersCurrentBasket.Items.Add(basketItem);
                }
                else
                {
                    var item = usersCurrentBasket.Items.Find(i => i.Id == basketItem.Id);
                    UpdateBasketItem(item, basketItem, user.Id);
                }
            }

            usersCurrentBasket.RecalculateTotals();

            await _basketsStore.Update(usersCurrentBasket, cancellationToken);
            return _mapper.Map<BasketDto>(await _basketsQueries.GetOrCreateCurrentUserBasketAsync(user.Id, cancellationToken));
        }

        private IEnumerable<BasketItem> ConvertToBasketItems(List<CreateBasketItemCommand> requestItems)
        {
            var items = new List<BasketItem>();
            foreach (var item in requestItems)
            {
                items.Add(new BasketItem()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl,
                    PromoCode = item.PromoCode,
                    InputData = ConvertToInputData(item.InputData)
                });
            }

            return items;
        }

        private BasketItemInputData ConvertToInputData(CreateBasketItemInputDataCommand itemInputData)
        {
            if (itemInputData == null)
                return null;
            return new BasketItemInputData()
            {
                LocationId = itemInputData.LocationId,
                LocalityId = itemInputData.LocalityId,
                StreetId = itemInputData.StreetId,
                StreetName = itemInputData.StreetName,
                PlotNo = itemInputData.PlotNo,
                PlotSize = itemInputData.PlotSize,
                PlotSizeMetric = itemInputData.PlotSizeMetric,
                Zoning = itemInputData.Zoning,
                DevelopmentPhase = itemInputData.DevelopmentPhase
            };
        }

        private static void UpdateBasketItem(BasketItem item, BasketItem basketItem, Guid currentUserId)
        {
            item?.Update(basketItem, currentUserId);
        }
        
        public async Task SendForgotPasswordEmailAsync()
        {

            await _emailService.SendMail("kokomeodk@gmail.com", "Subscription/Report/Valuation Purchase", null,
                _smtpOptions.Value,
                null,
                new EmailTemplate
                {
                    Data = new Dictionary<string, string>()
                    {
                        {"resetLink", "callbackUrl"},
                        {"logoImageUr","www.gosmartvalue.com/gosmartvalue.png"},
                        {"firstName", "user.FirstName"},

                        {"fastName", "user.LastName"}
                    },
                    Template = TemplateConstants.TemplateAccountPasswordReset
                });
        }
    }
}
