using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Models.GraphModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Services
{
    public interface IInstructionService
    {
        Task<Guid> CreateInstruction(InstructionModel instructionModel);
        Task ReIssueInstruction(InstructionModel instruction);
        ICollection<InstructionModel> GetInstructions(User user, IList<InstructionStatus> statuses = null);
        InstructionModel GetInstruction(User user, Guid id);
        Task RejectInstruction(User requester, InstructionModel instruction);
        Task AcceptInstruction(User requester, Guid id);
        Task ConfirmInstructionAppointment(User requester, Guid id);
        IEnumerable<InstructionModel> GetUserInstructions(User currentUser);
        IEnumerable<CorporateInstructionPerValuerViewModel> GetCorporateUserInstructions(User currentUser);
        Task<IList<GetStandardUserInsructionViewModel>> GetStandardUserInsructions(Guid id);
    }
}
