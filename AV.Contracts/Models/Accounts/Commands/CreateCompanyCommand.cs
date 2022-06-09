using System;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class CreateCompanyCommand: IRequest<CompanyModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanyType Type { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
