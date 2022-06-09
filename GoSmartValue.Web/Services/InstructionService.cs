using AutoMapper;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces.Services;
using AV.Common.Interfaces.UnitOfWorks;
using GoSmartValue.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Common.Interfaces;
using AV.Contracts;
using AV.Contracts.Models;
using AV.Contracts.Models.Users;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Models.GraphModels;
using User = AV.Common.Entities.User;
using AV.Contracts.Enums;

namespace GoSmartValue.Web.Services
{
    public class InstructionService : IInstructionService
    {
        private readonly IInstructionUoW _instructionUoW;
        private readonly IUserManagerService _userManagerService;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IEmailService _emailService;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IMapper _mapper;

        public InstructionService(IInstructionUoW instructionUoW,
            IUserManagerService userManagerService,
            IOptions<SmtpConfiguration> smtpOptions,
            IAccountsRepository accountsRepository,
            IEmailService emailService,
            IPaymentsRepository paymentsRepository,
            IMapper mapper)
        {
            _instructionUoW = instructionUoW;
            _userManagerService = userManagerService;
            _smtpOptions = smtpOptions;
            _accountsRepository = accountsRepository;
            _emailService = emailService;
            _paymentsRepository = paymentsRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateInstruction(InstructionModel instructionModel)
        {
            var instruction = _mapper.Map<Instruction>(instructionModel);
            if (instructionModel.ValuerAccountId.HasValue)
            {
                instruction.Valuer = _accountsRepository.Get(instructionModel.ValuerAccountId.Value).User;
            }

            _instructionUoW.CreateInstruction(instruction);
            await SendInstructionCreatedEmails(instruction);
            return instruction.Id;

        }

        public ICollection<InstructionModel> GetInstructions(User user, IList<InstructionStatus> statuses = null)
        {
            var instructions = _instructionUoW.GetInstructions(user, statuses);
            ICollection<InstructionModel> mappedInstructions = new List<InstructionModel>();
            foreach (var instruction in instructions)
            {
                var mappedInstruction = _mapper.Map<InstructionModel>(instruction);
                mappedInstruction.Valuation = _mapper.Map<ValuationModel>(instruction.Valuation);

                if (mappedInstruction.Valuation != null)
                {
                    mappedInstruction.Valuation.IsPaid = ValuationIsPaid(mappedInstruction);
                }
 
                mappedInstructions.Add(mappedInstruction);
            }

            return mappedInstructions;
        }

        private bool ValuationIsPaid(InstructionModel mappedInstruction)
        {
            if (mappedInstruction.Valuation == null)
            {
                return false;
            }
             return _paymentsRepository.GetAll()
                 .Any(p =>
                     p.Type == PaymentType.DetailedReport
                     && p.Status == PaymentStatus.Paid
                     && string.Equals(p.Reference, GetValuationId(mappedInstruction),
                         StringComparison.CurrentCultureIgnoreCase));
            
        }


        private static string GetValuationId(InstructionModel mappedInstruction)
        {
            if (mappedInstruction.Valuation == null)
            {
                return string.Empty;
            }

            return mappedInstruction.Valuation.Id.ToString();
        }
        

        public InstructionModel GetInstruction(User user, Guid id)
        {
            var instruction = _instructionUoW.GetInstruction(user, id);
            return _mapper.Map<InstructionModel>(instruction);
        }

        public async Task RejectInstruction(User requester, InstructionModel instructionModel)
        {
            var instruction = _mapper.Map<Instruction>(instructionModel);

            _instructionUoW.RejectInstruction(requester, instruction);

            await SendInstructionRejectedEmails(requester, instruction);
        }

        public async Task AcceptInstruction(User requester, Guid id)
        {
            var instruction = _instructionUoW.AcceptInstruction(requester, id);
            var subject = "Valuation Instruction Accepted";
            var dashboardLink = $"corporate/pending";
            await SendInstructionEmails(requester, instruction.IssuerId, subject, TemplateConstants.TemplateAcceptInstruction, dashboardLink);
        }

        public async Task ConfirmInstructionAppointment(User requester, Guid id)
        {
            var instruction = _instructionUoW.ConfirmInstructionAppointment(requester, id);
            var subject = "Instruction Booking Confirmed";
            var dashboardLink = $"corporate/pending";
            await SendInstructionEmails(requester, instruction.IssuerId, subject, TemplateConstants.TemplateConfirmedInstruction, dashboardLink);
        }

        public async Task ReIssueInstruction(InstructionModel instructionModel)
        {
            var instruction = _mapper.Map<Instruction>(instructionModel);

            _instructionUoW.ReIssueInstruction(instruction);

            await SendInstructionCreatedEmails(instruction);
        }

        private async Task<AV.Contracts.Models.Users.UserModel> GetUserDetails(Guid userId)
        {
            return await _userManagerService.GetUserDetails(userId);
        }

        private async Task<AV.Contracts.Models.Users.UserModel> GetValuerDetails(Instruction instruction)
        {
            if(instruction.ValuerId != default)
                return await GetUserDetails(instruction.ValuerId);
            return _mapper.Map<AV.Contracts.Models.Users.UserModel>(_instructionUoW.GetValuer(instruction));
        }

        private async Task SendInstructionCreatedEmails(Instruction instruction)
        {
            var valuer = await GetValuerDetails(instruction);
            var corporateUser = await GetUserDetails(instruction.IssuerId);
            await SendNewInstructionEmailToValuer(valuer, instruction);
            await SendNewInstructionEmailToCorporate(corporateUser, instruction);
        }

        private async Task SendNewInstructionEmailToValuer(AV.Contracts.Models.Users.UserModel valuer, Instruction instruction)
        {
            var data = new EmailTemplate
            {
                Data = new Dictionary<string, string>
                {
                    {"corporateClientName", $"{instruction.Issuer.FirstName} {instruction.Issuer.LastName}"},
                    {"dashboardLink", "www.gosmartvalue.com/valuer/Newinstructions"}
                },
                Template = TemplateConstants.TemplateNewInstructionValuer
            };

            await _emailService.SendMail(valuer.UserName,
                "New instruction for valuation", 
                null,
                _smtpOptions.Value,
                Constants.FromAddress,
                data);
        }

        private async Task SendNewInstructionEmailToCorporate(AV.Contracts.Models.Users.UserModel corporate, Instruction instruction)
        {
            var data = new EmailTemplate
            {
                Data = new Dictionary<string, string>
                {
                    {"dashboardLink", "www.gosmartvalue.com/corporate/pendinginstruction"}
                },
                Template = TemplateConstants.TemplateNewInstructionIssuer
            };

            await _emailService.SendMail(corporate.UserName,
                "New instruction for valuation sent",
                null,
                _smtpOptions.Value,
                Constants.FromAddress,
                data);
        }

        private async Task SendInstructionRejectedEmails(User requester, Instruction instruction)
        {
            //Send email confirmation to corporate user
            var data = new EmailTemplate
            {
                Data = new Dictionary<string, string>
                {
                    {"valuerName", $"{requester.FirstName} {requester.LastName}"},
                    {"dashboardLink", "www.gosmartvalue.com/valuer/Newinstructions"}
                },
                Template = TemplateConstants.TemplateRejectInstruction
            };

            var receipient = await GetUserDetails(instruction.IssuerId);
            await _emailService.SendMail(receipient.UserName,
                    "New instruction for valuation", null,
                    _smtpOptions.Value,
                    Constants.FromAddress,
                    data);
        }

        private async Task SendInstructionEmails(User requester, Guid sendUserId, string subject, string template, string dashboardLink)
        {
            var data = new EmailTemplate
            {
                Data = new Dictionary<string, string>
                {
                    {"valuerName", $"{requester.FirstName} {requester.LastName}"},
                    {"dashboardLink", $"{Startup.Hostname}/{dashboardLink}"}
                },
                Template = template
            };

            var receipient = await GetUserDetails(sendUserId);
            await _emailService.SendMail(
                receipient.UserName, 
                subject, 
                null, 
                _smtpOptions.Value,
                Constants.FromAddress,
                data);
        }

		public IEnumerable<InstructionModel> GetUserInstructions(User user)
		{
            var instructions = _instructionUoW.GetUserInstructions(user);
            ICollection<InstructionModel> mappedInstructions = new List<InstructionModel>();
            foreach (var instruction in instructions)
            {
                var mappedInstruction = _mapper.Map<InstructionModel>(instruction);
                mappedInstruction.Valuation = _mapper.Map<ValuationModel>(instruction.Valuation);

                if (mappedInstruction.Valuation != null)
                {
                    mappedInstruction.Valuation.IsPaid = ValuationIsPaid(mappedInstruction);
                }

                mappedInstructions.Add(mappedInstruction);
            }

            return mappedInstructions;
        }

        public IEnumerable<CorporateInstructionPerValuerViewModel> GetCorporateUserInstructions(User currentUser)
        {
            var instructions = _instructionUoW.GetCorporateInstructions(currentUser.Id);

            var valuerStatsDictionary = new Dictionary<Guid, int>();
            var corporateInstructions = new List<CorporateInstructionPerValuerViewModel>();
            foreach (var instruction in instructions)
            {
                if (!valuerStatsDictionary.ContainsKey(instruction.ValuerId))
                {
                    valuerStatsDictionary.Add(instruction.ValuerId, 0);
                    corporateInstructions
                        .Add(new CorporateInstructionPerValuerViewModel
                        {
                            ValuerName = $"{instruction.Valuer.FirstName} { instruction.Valuer.LastName }",
                            Count = instructions.Count(i => i.ValuerId == instruction.ValuerId)
                        });
                }
            }
            return corporateInstructions;

        }
	
	    public async Task<IList<GetStandardUserInsructionViewModel>> GetStandardUserInsructions(Guid id)
        {
            var instructions = await _instructionUoW.GetInstructionsByIssure(id);
            return instructions.Select(instruction => new GetStandardUserInsructionViewModel
            {
                InstructionId = instruction.Id,
                CreatedOn = instruction.CreatedDate,
                DateOfVisit = instruction.PreferredAccessDate,
                LocationName = instruction.LocationName,
                PlotNo = instruction.PlotNumber,
                Status = instruction.Status,
                Valuer = new ValuerViewModel
                {
                    FirstName = instruction.Valuer?.FirstName,
                    LastName = instruction.Valuer?.LastName,
                    CompanyId = GetCompany(instruction.Valuer?.Accounts)?.Id,
                    AgencyName= GetCompany(instruction.Valuer?.Accounts)?.Name,
                    LogoUrl = GetCompany(instruction.Valuer?.Accounts)?.LogoUrl,
                    Id = instruction.ValuerId
                 }
            }).ToList() ;
        }

        private Company GetCompany(ICollection<AV.Common.Entities.Account> acc)
        {
            return acc.FirstOrDefault().Company;
        }
       
    }
}