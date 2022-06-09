using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoles = AV.Common.Constants.UserRoles;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class InstructionUoW : UnitOfWork, IInstructionUoW
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserManagerUnitOfWork _customUserManager;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IInstructionRepository _iInstructionRepo;
        private readonly ILocationsRepository _locationsRepository;
        private readonly ILogger _logger;

        public InstructionUoW(IdentityDbContext<User, Role, Guid> dbContext,
            UserManager<User> userManager,
            IUserManagerUnitOfWork customUserManager,
            IAccountsRepository accountsRepository,
            IInstructionRepository iInstructionRepo,
            ILocationsRepository locationsRepository) : base(dbContext)
        {
            _userManager = userManager;
            _customUserManager = customUserManager;
            _accountsRepository = accountsRepository;
            _iInstructionRepo = iInstructionRepo;
            _locationsRepository = locationsRepository;
            _logger = new LoggerFactory().CreateLogger(typeof(InstructionUoW));
        }

        public void CreateInstruction(Instruction instruction)
        {
            //validate
            if (instruction.AccountId == default || instruction.Account == null)
            {
                //Associate the Corporate/Valuer Account to instruction
                instruction.Account = GetAccount(instruction.IssuerId);
            }

            ResolveLocations(instruction);
            var validationResult = CreateInstructionValidator(instruction);
            if (!validationResult.IsValid)
            {
                foreach (var message in validationResult.Messages)
                {
                    _logger.Log(LogLevel.Error, message);
                }
                throw new GoSmartValueException("Instruction is invalid.");
            }

            //instruction.UpdatedBy = instruction.IssuerId;
            instruction.CanBeReIssued = true;
            _iInstructionRepo.Add(instruction);
            Complete();
        }

        private void ResolveLocations(Instruction instruction)
        {
            Location location = null;
            if (instruction.LocationId > 0)
            {
                location = _locationsRepository.Get(instruction.LocationId);
            }

            if (!string.IsNullOrEmpty(instruction.LocationName)
                && instruction.LocationId == default)
            {
                location = _locationsRepository.AddLocation(instruction.LocationName);
            }

            if (location == null)
                return;
            instruction.LocationId = location.Id;

            Locality locality = null;
            if (instruction.LocalityId > 0)
            {
                locality = _dbContext.Set<Locality>().SingleOrDefault(l => l.Id == instruction.LocalityId);
            }

            if (!string.IsNullOrEmpty(instruction.LocalityName?.Trim()) && (instruction.LocalityId == 0 || instruction.LocalityId == null))
            {
                locality = _locationsRepository.AddLocality(location.Id, instruction.LocalityName);
            }

            if (locality == null)
                return;
            instruction.LocalityId = locality.Id;
            Street street = null;
            if (!string.IsNullOrEmpty(instruction.StreetName))
            {
                street = _locationsRepository
                        .AddStreetName(location.Id, locality.Id, instruction.StreetName);
            }

            if (street != null)
            {
                instruction.StreetId = street.Id;
            }
        }

        public void ReIssueInstruction(Instruction instructionFromUser)
        {
            var instructionFromStorage = _iInstructionRepo.Get(instructionFromUser.Id);
            //validate
            if (instructionFromStorage.AccountId == default || instructionFromStorage.Account == null)
                //Associate the Corporate Account to instruction
                instructionFromStorage.Account = GetAccount(instructionFromStorage.IssuerId);
            if (instructionFromStorage.ParentInstructionId.HasValue
                && instructionFromStorage.ParentInstructionId.Value != default
                && !instructionFromStorage.CanBeReIssued)
            {
                throw new GoSmartValueException("This instruction has already been linked to another instruction.");
            }

            instructionFromStorage.CanBeReIssued = false;
            instructionFromStorage.UpdatedBy = instructionFromUser.IssuerId;
            UpdateInstruction(instructionFromStorage);

            var newInstruction = GenerateInstruction(instructionFromUser, instructionFromStorage);

            CreateInstruction(newInstruction);
        }

        private static Instruction GenerateInstruction(Instruction originalInstruction, Instruction instructionFromStorage)
        {
            return new Instruction
            {
                IssuerId = originalInstruction.IssuerId,
                ValuerId = originalInstruction.ValuerId,
                ParentInstructionId = originalInstruction.Id,
                PreferredAccessDate = originalInstruction.PreferredAccessDate,
                UpdatedBy = originalInstruction.IssuerId,
                AccountId = instructionFromStorage.AccountId,
                SupportingDocuments = instructionFromStorage.SupportingDocuments,
                AccessContactMobileNumber = instructionFromStorage.AccessContactMobileNumber,
                AccessContactName = instructionFromStorage.AccessContactName,
                ClientName = instructionFromStorage.ClientName,
                ClientMobileNumber = instructionFromStorage.ClientMobileNumber,
                LocalityId = instructionFromStorage.LocalityId,
                LocationId = instructionFromStorage.LocationId,
                PlotNumber = instructionFromStorage.PlotNumber
            };
        }

        public void RejectInstruction(User requester, Instruction instructionFromClient)
        {
            var instructionTrackingChanges = _iInstructionRepo.Get(instructionFromClient.Id);
            //validate
            if (instructionTrackingChanges.AccountId == default || instructionTrackingChanges.Account == null)
                //Associate the Corporate Account to instruction
                instructionTrackingChanges.Account = GetAccount(instructionTrackingChanges.IssuerId);

            instructionTrackingChanges.UpdatedBy = requester.Id;
            instructionTrackingChanges.Status = InstructionStatus.Rejected;
            instructionTrackingChanges.Comment = instructionFromClient.Comment;
            UpdateInstruction(instructionTrackingChanges);
        }

        public Instruction AcceptInstruction(User requester, Guid id)
        {
            var instruction = _iInstructionRepo.Get(id);
            //validate
            if (instruction.ValuerId != requester.Id)
                throw new GoSmartValueException("The user account is not the assigned valuer.");
            if (instruction.AccountId == default || instruction.Account == null)
                //Associate the Corporate Account to instruction
                instruction.Account = GetAccount(instruction.IssuerId);
            instruction.UpdatedBy = requester.Id;
            instruction.ValuerAccepted = true;
            instruction.Status = InstructionStatus.Pending;
            UpdateInstruction(instruction);
            return instruction;
        }

        public Instruction ConfirmInstructionAppointment(User requester, Guid id)
        {
            var instruction = _iInstructionRepo.Get(id);
            //validate
            if (instruction.Status != InstructionStatus.Pending || !instruction.ValuerAccepted)
                throw new GoSmartValueException("The instruction must be in a pending state. " +
                                                $"instruction must be accepted. #instruction:{id}");
            if (instruction.ValuerId != requester.Id)
                throw new GoSmartValueException("The user account is not the assigned valuer.");
            if (instruction.AccountId == default || instruction.Account == null)
                //Associate the Corporate Account to instruction
                instruction.Account = GetAccount(instruction.IssuerId);
            instruction.UpdatedBy = requester.Id;
            instruction.Status = InstructionStatus.Confirmed;
            UpdateInstruction(instruction);
            return instruction;
        }

        public User GetValuer(Instruction instruction)
        {
            if (instruction.ValuerId != default)
                return instruction.Valuer;
            return _accountsRepository
                .Get(instruction.AccountId)
                .User;
        }

        public void UpdateInstruction(Instruction instruction)
        {
            var result = CreateInstructionValidator(instruction);
            if (!result.IsValid)
            {
                foreach (var message in result.Messages)
                {
                    _logger.Log(LogLevel.Error, message);
                }
                throw new GoSmartValueException($"Instruction is invalid. " +
                                                $"{Environment.NewLine}Validation Messages:" +
                                                $"{Environment.NewLine}ClassName: {GetType().FullName}" +
                                                $"{Environment.NewLine}{result.Messages}");
            }

            if (instruction.Id == default)
            {
                _logger.Log(LogLevel.Error, $"Instruction Id is invalid. #instruction:{instruction.Id}");
                throw new GoSmartValueException($"Instruction is invalid. #instruction:{instruction.Id}");
            }
            _iInstructionRepo.Update(instruction);
            Complete();
        }

        public ICollection<Instruction> GetInstructions(User requester, IList<InstructionStatus> status = null)
        {
            if (requester.Accounts == null)
            {
                requester.Accounts = _customUserManager.GetUserAccounts(requester.Id);
            }

            var result = GetInstructionValidator(requester);
            if (!result.IsValid)
            {
                foreach (var message in result.Messages)
                {
                    _logger.Log(LogLevel.Error, message);
                }
                throw new GoSmartValueException($"Instruction request is invalid. => " +
                                                $"{Environment.NewLine}ClassName: {GetType().FullName}" +
                                                $"{Environment.NewLine}Error Details: {result.Messages}");
            }
            // determine account type e.g Corporate or valuer
            var account = requester.Accounts
                .First(a => a.Active && a.IsCorporate || a.IsValuer || a.AccountType == AccountType.Standard);

            return GetFilteredInstruction(account.Id, requester.Id, status);
        }

        public Instruction GetInstruction(User user, Guid id)
        {
            var instruction = _iInstructionRepo.Find(i => i.Id == id)
                    .Include(i => i.Valuer)
                    .Include(i => i.Valuation)
                    .Include(i => i.Issuer)
                    .Include(i => i.Account)
                    .Include(i => i.Location)
                    .Include(i => i.Locality)
                    .SingleOrDefault();

            var userAccount = GetAccount(user.Id);
            if (instruction != null && (instruction.Account.Id == userAccount.Id || instruction.ValuerId == user.Id))
            {
                return instruction;
            }
            throw new GoSmartValueException("You are not permitted to access this instruction.");
        }

        public Instruction GetInstruction(Guid id)
        {
            return _iInstructionRepo.Find(i => i.Id == id)
                    .Include(i => i.Valuer)
                    .Include(i => i.Valuation)
                    .Include(i => i.Issuer)
                    .Include(i => i.Account)
                    .Include(i => i.Location)
                    .Include(i => i.Locality)
                    .SingleOrDefault();
        }

        private ICollection<Instruction> GetFilteredInstruction(Guid accountId, Guid requesterId, IList<InstructionStatus> statuses)
        {
            //get all instructions on account/ include
            var instructions = _iInstructionRepo
                .Find(ins => ins.AccountId == accountId
                    || ins.ValuerId == requesterId)
                .AsNoTracking()
                .Include(i => i.SupportingDocuments)
                .Include(i => i.Valuation)
                .Include(i => i.Issuer)
                .Include(i => i.Valuer)
                .Include(i => i.Account)
                .Include(i => i.Location)
                .Include(i => i.Locality)
                .ToList()
                .Where(ins => statuses.Count == 0 || statuses.Contains(ins.Status))
                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            return instructions;
        }

        private ICollection<Instruction> GetFilteredInstruction(Guid accountId, Guid requesterId)
        {
            //get all instructions on account/ include
            var instructions = _iInstructionRepo
                .Find(ins => ins.AccountId == accountId
                    || ins.ValuerId == requesterId)
                .AsNoTracking()
                .Include(i => i.SupportingDocuments)
                .Include(i => i.Valuation)
                .Include(i => i.Issuer)
                .Include(i => i.Valuer)
                .Include(i => i.Account)
                .Include(i => i.Location)
                .Include(i => i.Locality)

                .ToList()

                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            return instructions;
        }

        public IEnumerable<Instruction> GetCorporateInstructions(Guid corporateUserId)
        {
            var account = _customUserManager.GetUserAccounts(corporateUserId)
                .First(u => u.Active);

            //get all instructions on account/ include
            var instructions = _iInstructionRepo
                //All instructions for my corporate account
                .Find(ins => ins.AccountId == account.Id
                    && ins.IssuerId == corporateUserId)
                .Include(i => i.Valuer);
            return instructions;
        }

        public ValidationResult GetInstructionValidator(User requester)
        {
            var result = new ValidationResult();

            if (requester == null)
            {
                result.AddMessage("Requester(user) should not be null.");
                return result;
            }

            if (!requester.Active)
            {
                result.AddMessage("Requester(user) is not active.");
                return result;
            }

            if (requester.Accounts == null || requester.Accounts.Count() == 0)
            {
                result.AddMessage("Requester has no active transaction account.");
                return result;
            }

            if (requester.Accounts.Count(ac => ac.Active) == 0)
            {
                result.AddMessage("There is no active transaction account.");
            }

            if (requester.Accounts.Count(ac => ac.Active && ac.IsCorporate || ac.IsValuer || ac.AccountType == AccountType.Standard || IsGosmartValueStaff(requester)) == 0)
            {
                result.AddMessage("There is no Corporate/Valuer/Standard Transaction account.");
            }

            return result;
        }

        private bool IsGosmartValueStaff(User user)
        {
            var roles = new List<string>();
            Task.Run(async () =>
            {
                roles.AddRange(await _userManager.GetRolesAsync(user));
            });
            return roles.Any(r => string.Equals(r, UserRoles.Admin, StringComparison.InvariantCultureIgnoreCase)
                                  || string.Equals(r, UserRoles.Analyst, StringComparison.InvariantCultureIgnoreCase));

        }

        public ValidationResult CreateInstructionValidator(Instruction instruction)
        {
            var result = new ValidationResult();

            if (instruction == null)
            {
                result.AddMessage("Instruction should not be null.");
                return result;
            }

            if (instruction.IssuerId == default)
            {
                result.AddMessage("IssuerId must be set to a corporate user.");
            }

            if (!HasActiveAccount(instruction.Account))
            {
                result.AddMessage("Issuer Account is not active.");
            }

            return result;
        }

        private bool HasActiveAccount(Account account)
        {
            if (account == null)
                return false;
            if (!account.Active /*|| (!account.IsCorporate && !account.IsValuer)*/)
            {
                return false;
            }
            return true;
        }

        private Account GetAccount(Guid userId)
        {
            var account = _customUserManager.GetUserAccounts(userId)
                .FirstOrDefault(ac => ac.Active);
            if (account.AccountType != AccountType.Corporate)
            {
                if (account != default)
                    _dbContext.Entry(account).State = EntityState.Unchanged;
            }
            return account;
        }

        public ICollection<Instruction> GetUserInstructions(User user)
        {
            if (user.Accounts == null)
            {
                user.Accounts = _customUserManager.GetUserAccounts(user.Id);
            }

            var result = GetInstructionValidator(user);
            if (!result.IsValid)
            {
                foreach (var message in result.Messages)
                {
                    _logger.Log(LogLevel.Error, message);
                }
                throw new GoSmartValueException($"Instruction request is invalid. => " +
                                                $"{Environment.NewLine}ClassName: {GetType().FullName}" +
                                                $"{Environment.NewLine}Error Details: {result.Messages}");
            }
            // determine account type e.g Corporate or valuer
            var account = user.Accounts
                .First(a => a.Active && a.IsCorporate || a.IsValuer || a.AccountType == AccountType.Standard);

            return GetFilteredInstruction(account.Id, user.Id);
        }

        public async Task<IList<Instruction>> GetInstructionsByIssure(Guid IssureId)
        {
            return await _dbContext.Set<Instruction>()
                .Where(x => x.IssuerId == IssureId)
                .Include(x => x.Valuer)
                .ThenInclude(x => x.Accounts)
                .ThenInclude(x => x.Company)
                .ToListAsync();
        }
    }
}