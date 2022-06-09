using AV.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class Instruction : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid IssuerId { get; set; }
        public virtual User Issuer { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string LocationName { get; set; }
        public Guid ValuerId { get; set; }
        public User Valuer { get; set; }
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public string LocalityName { get; set; }
        public IEnumerable<InstructionDocument> SupportingDocuments { get; set; }
        public InstructionStatus Status { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public bool ValuerAccepted { get; set; }
        public ICollection<InstructionHistory> History { get; set; }
        public ICollection<InstructionAppointment> Appointments { get; set; }
        public string Comment { get; set; }
        public string PlotNumber { get; set; }
        public DateTime? PreferredAccessDate { get; set; }
        public string AccessContactName { get; set; }
        public string AccessContactMobileNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientMobileNumber { get; set; }
        public string ClientOrganisationName { get; set; }
        public Guid? ParentInstructionId { get; set; }
        public virtual Instruction ParentInstruction { get; set; }

        [ForeignKey("Valuation")]
        public Guid? ValuationId { get; set; }

        public Valuation Valuation { get; set; }
        public bool CanBeReIssued { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string StreetName { get; set; }
        public int StreetId { get; set; }
        public string WhatsAppNumber { get; set; }
        public bool IsVideoWalkthroughPossible { get; set; }
        public double Cost { get; set; }
        public PaymentStatus paymentStatus { get; set; }
    }
}