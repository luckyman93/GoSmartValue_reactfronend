using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class ComparableResult
    {
        [Key]
        public Guid ReferenceNumber { get; set; }

        public Guid? RequesterUserId { get; set; }
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

        [ForeignKey("Valuation")]
        public Guid? ValuationId { get; set; }

        public Valuation Valuation { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        [ForeignKey("Comparable")]
        public Guid ComparableId { get; set; }

        public virtual Comparable Comparable { get; set; }
        public IEnumerable<ComparableResultComparable> Comparables { get; set; }

        public bool SwimmingPool { get; set; }
        public bool FirePlace { get; set; }
        public bool BoundaryWall { get; set; }
        public bool ElectricFence { get; set; }
        public bool MotorizedGate { get; set; }
        public bool OutdoorEntertainmentArea { get; set; }
        public bool Paved { get; set; }
        public string OtherSpecialFeatures { get; set; }
    }
}