using AV.Contracts.Models.Payment.Models;
using MediatR;
using System;
using Newtonsoft.Json;

namespace AV.Contracts.Models.Basket.Commands;

public class ProcessSuccessFullPaymentCommand : IRequest<CellulantCallBackRequestDTO>
{
    public Guid UserId { get; set; }
    [JsonIgnore] 
    public SuccessFullPaymentCommand SuccessFullPaymentCommand { get; set; }
}