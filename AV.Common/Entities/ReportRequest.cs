using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class ReportRequest : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ReferenceNumber { get; set; }
        public Guid RequesterUserId { get; set; }
        public DateTimeOffset EstimatedOn { get; set; }
        public double EstimatedValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }
        public int BathRooms { get; set; }
        public bool ReportRequested { get; set; }
        public string ReportType { get; set; }
        public ICollection<PropertyFeature> Features { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public RequestStatus Status { get; set; }
    }
}