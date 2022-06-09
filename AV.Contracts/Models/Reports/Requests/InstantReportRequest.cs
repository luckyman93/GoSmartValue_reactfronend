using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Valuation;
using MediatR;


namespace AV.Contracts.Models.Reports.Requests
{
    public class InstantReportRequest : IRequest<PaymentTrack>
    {
        public InstantReportRequest(Users.UserModel currentUser, CreateInstantReportCommand reportRequest = null, InstructionModel instruction = null)
        {
            ReportRequest = reportRequest;
            User = currentUser;
            InstructionRequest = instruction;
        }

        public CreateInstantReportCommand ReportRequest { get; private set; }
        public InstructionModel InstructionRequest { get; private set; }

        public Users.UserModel User { get; }
    }
}
