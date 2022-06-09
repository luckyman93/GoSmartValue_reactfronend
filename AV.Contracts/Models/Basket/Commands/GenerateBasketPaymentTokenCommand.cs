using AV.Contracts.Models.Payment.Models;
using MediatR;
using System;
using Newtonsoft.Json;

namespace AV.Contracts.Models.Basket.Commands;

public class GenerateBasketPaymentTokenCommand : IRequest<PaymentTokenDto>
{
    public Guid UserId { get; set; }
    [JsonIgnore] 
    public CreateBasketTokenCommand CreateBasketTokenCommand { get; set; }
}