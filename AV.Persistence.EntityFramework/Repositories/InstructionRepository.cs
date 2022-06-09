using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using System;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class InstructionRepository : Repository<Instruction>, IInstructionRepository
    {
        private readonly IAccountsRepository _accountsRepository;
        public InstructionRepository(ValuationsContext context, IAccountsRepository accountsRepository) : base(context)
        {
            _accountsRepository = accountsRepository;
        }
        public async Task<string> UpdateInsructionPaymentStatus(Guid instructionId)
        {
            var instruction = await DbContext.Set<Instruction>().FindAsync(instructionId);
            instruction.paymentStatus = PaymentStatus.Paid;
            try
            {
                await DbContext.SaveChangesAsync();
                return "saved";
            }
            catch
            {
                return "failed to update instruction status";
            }
        }
        public async Task<Guid> SaveInstruction(Instruction newInstruction)
        {
            var account = await _accountsRepository.GetUserAccount(newInstruction.ValuerId);
            newInstruction.AccountId = account.Id;
            await DbContext.Set<Instruction>().AddAsync(newInstruction);
            await DbContext.SaveChangesAsync();
            return newInstruction.Id;
        }
    }
}