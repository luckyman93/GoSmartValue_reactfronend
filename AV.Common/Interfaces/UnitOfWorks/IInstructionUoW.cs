using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Contracts.Enums;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IInstructionUoW
    {
        void CreateInstruction(Instruction instruction);
        ValidationResult CreateInstructionValidator(Instruction instruction);
        void UpdateInstruction(Instruction instruction);
        ICollection<Instruction> GetInstructions(User user, IList<InstructionStatus> status = null);
        Instruction GetInstruction(User user, Guid id);
        Instruction GetInstruction(Guid instructionId);
        void ReIssueInstruction(Instruction instructionFromUser);
        void RejectInstruction(User requester, Instruction instructionFromClient);
        Instruction AcceptInstruction(User requester, Guid id);
        Instruction ConfirmInstructionAppointment(User requester, Guid id);
        User GetValuer(Instruction instruction);
        ICollection<Instruction> GetUserInstructions(User user);
		IEnumerable<Instruction> GetCorporateInstructions(Guid corporateUserId);
        Task<IList<Instruction>> GetInstructionsByIssure(Guid IssureId);
        
	}
}
