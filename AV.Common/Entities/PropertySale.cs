using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class PropertySale
    {
        [Key]
        public Guid Id { get; set; }
        private DateTimeOffset addedOn;
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTimeOffset AddedOn
        {
            get
            {
                if (addedOn == default)
                {
                    addedOn = DateTimeOffset.UtcNow;
                }

                return addedOn;
            }
            set => addedOn = value;
        }
        public Guid AddedBy { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; } = DateTimeOffset.UtcNow;
        public DataState DataState { get; set; } = DataState.Raw;
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTimeOffset? DateOfSale { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime? Date { get; set; }
        public TransactionType? TransactionType { get; set; }
        public string BondNumber { get; set; }
        public decimal? BondAmount { get; set; }
        public string BankName { get; set; }
        public virtual Seller Seller { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public virtual Buyer Buyer { get; set; }
        //Property
        public Metric Metric { get; set; }
        public decimal? PlotSize { get; set; }
        public int? PlotId { get; set; }
        public virtual Plot Plot { get; set; }
        public LandUse? LandUse { get; set; }
        public PropertyType? PropertyType { get; set; }
        public virtual ComparableBandSize BandClass { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public int? StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ICollection<PropertyFeature> Features { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
    }
}