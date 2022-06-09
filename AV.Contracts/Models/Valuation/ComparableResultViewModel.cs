using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Valuation
{
    public class ComparableResultViewModel
    {
        [DataType(DataType.Text)]
        public double EstimatedValue { get; set; }
        public ComparableRequestViewModel ComparableRequest { get; set; }
        public Users.UserModel RequesterUser { get; set; }
        public Guid RequesterUserId { get; set; }
        public virtual Users.UserModel Owner { get; set; }
        public Guid ComparableResultId { get; set; }
        public bool ReportRequested { get; set; }
        public Guid ReferenceNumber { get; set; }
        public int PropertyDetailsId { get; set; }
        public virtual PropertySaleViewModel PropertySaleDetail { get; set; }
        public DateTimeOffset EstimatedOn { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }
        public int BathRooms { get; set; }
        public string ReportType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public List<FeatureType> PropertyFeatures { get; set; }
        public Guid ComparableId { get; set; }
        public ComparableViewModel Comparable { get; set; }
        public List<ComparableViewModel> Comparables { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
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