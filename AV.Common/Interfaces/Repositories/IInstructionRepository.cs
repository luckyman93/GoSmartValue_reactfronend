using AV.Common.Entities;
using System;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.Repositories
{
    public interface IInstructionRepository : IRepository<Instruction>
    {
        Task<string> UpdateInsructionPaymentStatus(Guid instructionId);
        Task<Guid> SaveInstruction(Instruction newInstruction);
    }
}