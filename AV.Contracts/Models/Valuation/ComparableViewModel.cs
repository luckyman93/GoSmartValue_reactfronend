using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Valuation
{
    public class ComparableViewModel
    {
        public Guid Id { get; set; }
        private DateTimeOffset _addedOn;
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTimeOffset AddedOn
        {
            get
            {
                if (_addedOn == default)
                {
                    _addedOn = DateTimeOffset.UtcNow;
                }

                return _addedOn;
            }
            set => _addedOn = value;
        }
        public Guid AddedBy { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; } = DateTimeOffset.UtcNow;
        public DataState DataState { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTimeOffset? DateOfSale { get; set; }
        public decimal SalePrice { get; set; }
        //Property
        public Metric Metric { get; set; }
        public decimal PlotSize { get; set; }
        public LandUse LandUse { get; set; }
        public PropertyType PropertyType { get; set; }
        public virtual ComparableBandSizeViewModel BandClass { get; set; }
        public string BandClassName { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public ICollection<PropertyFeatureModel> Features { get; set; }
        public ICollection<RoomsModel> Rooms { get; set; }
        public Guid? ComparableResultId { get; set; }
        public ComparableResultViewModel ComparableResult { get; set; }
        public virtual SellerModel Seller { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public virtual BuyerModel Buyer { get; set; }
        public Guid SellerId { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerMobileNumber { get; set; }
        public string SellerEmail { get; set; }
        public string SellerIdentityNumber { get; set; }
        public Guid BuyerId { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerMobileNumber { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerIdentityNumber { get; set; }
        public Salutation? SellerSalutation { get; set; }
        public Salutation? BuyerSalutation { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public TransactionType TransactionType { get; set; }
        public string BondNumber { get; set; }
        public decimal? BondAmount { get; set; }
        public string BankName { get; set; }
        public int BathRooms { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }

        public int? SubscriptionOptionId { get; set; }
        public Guid? CurrentUserAccount { get; set; }

        public double Cost { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ValuationSource ValuationSource { get; set; }
        public string TitleDeedNo { get; set; }
    }
}