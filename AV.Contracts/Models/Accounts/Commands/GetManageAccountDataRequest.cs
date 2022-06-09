using System;
using AV.Contracts.Enums;
using AV.Contracts.Models.ViewModels;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class GetManageAccountDataRequest : IRequest<ManageAccountViewModel>
    {
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
    }
}