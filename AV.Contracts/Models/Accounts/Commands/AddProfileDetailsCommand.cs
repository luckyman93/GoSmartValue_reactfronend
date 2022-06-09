using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AV.Contracts.Models.Accounts.Commands
{
	public class AddProfileDetailsCommand : IRequest<Account>
	{
        public AddProfileDetailsCommand()
        {
            LogoDocument = new List<IFormFile>();
        }
        public Guid AccountId { get; set; }
        public string CompanyName { get; set; }
        public ICollection<IFormFile> LogoDocument { get; set; }
	}
}
