using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Users;

namespace AV.Contracts.Models.Valuation
{
    public class InstructionModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        public Guid? ParentInstructionId { get; set; }
        public virtual InstructionModel ParentInstruction { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string LocationName { get; set; }
        public Guid? ValuerId { get; set; }
        public ValuerViewModel Valuer { get; set; }
        public string ValuerName { get; set; }
        public int? LocalityId { get; set; }
        public Locality Locality { get; set; }
        public string LocalityName { get; set; }
        public ICollection<InstructionDocumentModel> SupportingDocuments { get; set; }
        public InstructionStatus Status { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public bool ValuerAccepted { get; set; }
        public ICollection<InstructionHistoryModel> History { get; set; }
        public ICollection<InstructionAppointmentModel> Appointments { get; set; }

        [Required]
        public string PlotNumber { get; set; }

        [Required]
        public DateTime? PreferredAccessDate { get; set; }

        public string AccessContactName { get; set; }
        public string AccessContactMobileNumber { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ClientMobileNumber { get; set; }

        public string ClientOrganisationName { get; set; }
        public Guid IssuerId { get; set; }
        public Users.UserModel Issuer { get; set; }

        [DefaultValue(true)]
        public bool CanBeReIssued { get; set; }
        public string Comment { get; set; }
        public Guid? ValuationId { get; set; }
        public string MarketCommentary { get; set; }
        public DateTime ValuationDate { get; set; }
        public decimal? Value { get; set; }
        [NotMapped]
        public ValuationModel Valuation { get; set; }
        public string WhatsAppNumber { get; set; }
        public bool isVideoWakthroughPossible { get; set; }
        public bool HasWhatsapp => !string.IsNullOrEmpty(WhatsAppNumber);
        public Guid? ValuerAccountId { get; set; }

        public int? SubscriptionOptionId { get; set; }
        public Guid? CurrentUserAccount { get; set; }

        public double Cost { get; set; }
        public PaymentStatus paymentStatus { get; set; }
    }

    public class DocumentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int FileSizeMb { get; set; }
        public DocumentStreamModel DocumentStream { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
        public string Url { get; set; }
    }

    public class DocumentStreamModel
    {
        public Guid Id { get; private set; }
        public string MimeType { get; private set; }
        public byte[] FileStream { get; private set; }
    }
    public class InstructionDocumentModel : DocumentModel
    {
        [ForeignKey("Instruction")]
        public Guid InstructionId { get; set; }
        public virtual InstructionModel Instruction { get; set; }
    }
}