using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class Comparable
    {
        [Key]
        public Guid Id { get; set; }

        private DateTimeOffset _addedOn;

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
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

        public decimal? SalePrice { get; set; }

        //Property
        public Metric Metric { get; set; }

        public decimal PlotSize { get; set; }
        public virtual Plot Plot { get; set; }
        public LandUse LandUse { get; set; }
        public PropertyType PropertyType { get; set; }
        public virtual ComparableBandSize BandClass { get; set; }
        public string BandClassName => BandClass?.BandName;
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("Locality")]
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public string TitleDeedNo { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Buyer Buyer { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public ICollection<PropertyFeature> Features { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedOn { get; set; }
        public int? StreetId { get; set; }
        public int? PlotId { get; set; }
        public Salutation? SellerSalutation { get; set; }
        public Salutation? BuyerSalutation { get; set; }
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
        public double Cost { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ValuationSource ValuationSource { get; set; }
        public IEnumerable<ComparableResultComparable> ComparableResults { get; set; }
    }

    public class ComparableResultComparable
    {
        public Guid ComparableId { get; set; }
        public Comparable Comparable { get; set; }
        public Guid ComparableResultId { get; set; }
        public ComparableResult ComparableResult { get; set; }
    }
}