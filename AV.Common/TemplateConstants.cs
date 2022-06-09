using AV.Contracts;

namespace AV.Common
{
    public static class TemplateConstants
    {
        //Email Templates
        public static string TemplateAccountActivation => ResourceStore.UserAccount_Confirmation;
        public static string TemplateAccountPasswordReset => ResourceStore.UserAccount_PasswordReset;
        public static string TemplateNewInstructionValuer => ResourceStore.InstructionNew_Valuer;
        public static string TemplateNewInstructionIssuer => ResourceStore.InstructionNew_CorporateUser;
        public static string TemplateRejectInstruction => ResourceStore.InstructionRejected_Issuer;
        public static string TemplateAcceptInstruction => ResourceStore.InstructionAccepted_Corporate;
        public static string TemplateConfirmedInstruction => ResourceStore.InstructionBookingConfirmed_Corporate;
        public static string TemplateValuationAcceptedValuer => ResourceStore.ValuationAccepted_Client;

        // Document Templates
        public static string TemplateDetailedValuationReport => ResourceStore.DetailedValuationReport;
        public static string TemplateCompleteValuationIssuer => ResourceStore.CompleteValuationEmailBody_Issuer;

        public static string FromAddress => $"donotreply@gosmartvalue.com";
    }
}