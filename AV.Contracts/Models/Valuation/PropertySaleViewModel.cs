using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Valuation
{
    public class PropertySaleViewModel : PropertySalesMetaData
    {
        public Guid Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public new DateTimeOffset DateOfSale { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public new decimal SalePrice { get; set; }
        public new int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public int? StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string StreetName { get; set; }
        //user provided
        public new Metric Metric { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public new decimal PlotSize { get; set; }
        public int PlotId { get; set; }
        public new LandUse LandUse { get; set; }
        public new PropertyType PropertyType { get; set; }
        public virtual ComparableBandSizeViewModel BandClass { get; set; }
        public new string PlotNo { get; set; }
        public new double? Latitude { get; set; }
        public new double? Longitude { get; set; }
        public ICollection<PropertySaleViewModel> Comparables { get; set; }
        public new DateTime? Date { get; set; }
        public new TransactionType TransactionType { get; set; }
       
        public new decimal BondAmount { get; set; }
        public string BondNumber { get; set; }
        public string BankName { get; set; }
        //Seller Details
        public Guid SellerId { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerMobileNumber { get; set; }
        public new string SellerEmail { get; set; }
        public new Salutation? SellerSalutation { get; set; }
        public string SellerIdentityNumber { get; set; }
        public string SellerName { get; set; }
        //Buyer Details
        public string BuyerName { get; set; }
        public Guid BuyerId { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerMobileNumber { get; set; }
        public new string BuyerEmail { get; set; }
        public new Salutation? BuyerSalutation { get; set; }
        public string BuyerIdentityNumber { get; set; }
        public Guid AddedBy { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }
    }

    public class PropertySalesMetaData
    {
        [DataType(DataType.Date)]
        public DateTimeOffset AddedOn { get; set; }
        public DataState DataState { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTimeOffset DateOfSale { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Display(Name = "Units")]
        public Metric Metric { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:#.##}")]
        public decimal PlotSize { get; set; }
        [Required]
        public LandUse LandUse { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }
        
        [Required]
        public string PlotNo { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:#.#########}")]
        public double? Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.#########}")]
        public double? Longitude { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Date { get; set; }
        
        [Required]
        public TransactionType TransactionType { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal BondAmount { get; set; }
        //Seller Details
        [DataType(DataType.EmailAddress)]
        public string SellerEmail { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public Salutation? SellerSalutation { get; set; }
        //Buyer Details
        [DataType(DataType.EmailAddress)]
        public string BuyerEmail { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public Salutation? BuyerSalutation { get; set; }
    }
}