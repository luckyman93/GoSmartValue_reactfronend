using System;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class ConfirmUserAccountCommand
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}