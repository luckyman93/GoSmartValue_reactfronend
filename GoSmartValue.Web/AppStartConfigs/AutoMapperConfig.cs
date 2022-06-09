using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Subscriptions;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Contracts.Models.Payment.Models;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Payment.Responses;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Commands;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Users;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Models.ViewModels;
using GoSmartValue.Web.Models;
using GoSmartValue.Web.Models.GraphModels;
using GoSmartValue.Web.ViewModels;
using Account = AV.Common.Entities.Account;
using BankAccount = AV.Common.Entities.BankAccount;
using ComparableRequestViewModel = AV.Contracts.Models.Valuation.ComparableRequestViewModel;
using Country = AV.Common.Entities.Country;
using District = AV.Common.Entities.District;
using ImportHeader = AV.Common.Entities.ImportHeader;
using Locality = AV.Common.Entities.Locality;
using Location = AV.Common.Entities.Location;
using Phone = AV.Common.Entities.Phone;
using Street = AV.Common.Entities.Street;
using UserModel = AV.Contracts.Models.Users.UserModel;

namespace GoSmartValue.Web.AppStartConfigs
{
    public class AutoValuationProfile : Profile
    {
        public AutoValuationProfile()
        {
            CreateMap<CountryViewModel, Country>().ReverseMap();
            CreateMap<DistrictViewModel, District>().ReverseMap();
            CreateMap<LocationViewModel, Location>().ReverseMap();
            CreateMap<LocalityViewModel, Locality>().ReverseMap();
            CreateMap<StreetViewModel, Street>().ReverseMap();
            CreateMap<PlotViewModel, Plot>().ReverseMap();
            CreateMap<PaymentModel, PaymentHistory>().ReverseMap();
            CreateMap<SetProductAmountRequest, SetProductAmountResponse>().ReverseMap();

            CreateMap<Valuation, ValuationReportViewModel>();
            CreateMap<User, DetailedReportValuer>();
            CreateMap<Comparable, PropertyInformation>();
            CreateMap<Comparable, GetInstantReportPreView>();
            CreateMap<Account, UserActiveSubscriptionViewModel>();


            CreateMap<AV.Contracts.Models.Market.ResponseModels.MarketInformation, MarketInformation>().ReverseMap();
            CreateMap<AV.Contracts.Models.Market.LandRateModel, LandRate>().ReverseMap();
            CreateMap<AV.Contracts.Models.ImportHeader, ImportHeader>().ReverseMap();
            CreateMap<AV.Contracts.Models.Country, Country>().ReverseMap();
            CreateMap<AV.Contracts.Models.District, District>().ReverseMap();
            CreateMap<AV.Contracts.Models.Location, Location>().ReverseMap();
            CreateMap<AV.Contracts.Models.Locality, Locality>().ReverseMap();
            CreateMap<AV.Contracts.Models.UserModel, User>().ReverseMap();
            CreateMap<AV.Contracts.Models.Account, Account>().ReverseMap();
            CreateMap<ComparableResultsViewModel, ComparableResultsViewModel>().ReverseMap();
            CreateMap<AV.Contracts.Models.Market.BuildingCostsModel, BuildingCost>().ReverseMap();
            CreateMap<AV.Contracts.Models.Market.BuildingMaterialCostModel, BuildingMaterialCost>().ReverseMap();

            CreateMap<AmenityViewModel, Amenity>().ReverseMap();
            CreateMap<ShapeViewModel, PlotShape>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<UserDetailsModel, User>().ReverseMap();
            CreateMap<UserLoginViewModel, User>().ReverseMap();
            CreateMap<ValuerViewModel, User>().ReverseMap();
            CreateMap<RegisterUserViewModel, User>().ReverseMap();
            CreateMap<RegisterValuerViewModel, User>().ReverseMap();
            CreateMap<RegisterCorporateViewModel, User>().ReverseMap();

            // Baskets
            CreateMap<BasketDto, Basket>().ReverseMap();
            CreateMap<CreateBasketTokenCommand, Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<CreateBasketItemCommand, BasketItem>();
            CreateMap<BasketItemInputDataDto, BasketItemInputData>().ReverseMap();

            // Products -> Package -> Features
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductFeatureModel, ProductFeature>().ReverseMap();
            CreateMap<PackageModel, Package>().ReverseMap();
            CreateMap<CreatePackageCommand, Package>();
            CreateMap<CreatePackageFeatureCommand, PackageFeature>();
            CreateMap<SubscriptionModel, Account>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User))
                .ForMember(dest => dest.AccountType, opts => opts.MapFrom(src => src.AccountType))
                .ForMember(dest => dest.SubscriptionType, opts => opts.MapFrom(src => src.SubscriptionType))
                .ForMember(dest => dest.Active, opts => opts.MapFrom(src => src.Active))
                .ForMember(dest => dest.Cost, opts => opts.MapFrom(src => src.Cost))
                .ForMember(dest => dest.ExpiryDate, opts => opts.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
                .ForMember(dest => dest.Members, opts => opts.MapFrom(src => src.Members))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.InstantReportsLimit, opts => opts.MapFrom(src => src.InstantReportsLimit))
                .ForMember(dest => dest.DetailedReportsLimit, opts => opts.MapFrom(src => src.DetailedReportsLimit))
                .ReverseMap();
            CreateMap<SubscriptionOptionModel, SubscriptionOption>().ReverseMap();
            CreateMap<PackageFeature, PackageFeatureModel>().ReverseMap();
            CreateMap<PropertyFeature, PropertyFeatureModel>().ReverseMap();

            CreateMap<CompanyModel, Company>().ReverseMap();
            CreateMap<RoomsModel, Rooms>().ReverseMap();

            CreateMap<InstructionModel, Instruction>().ReverseMap().MaxDepth(3);
            CreateMap<InstructionDocument, InstructionDocumentModel>().ReverseMap();
            CreateMap<GraphInstructionCountViewModel, Instruction>().ReverseMap().MaxDepth(3);
            CreateMap<ValuationModel, Valuation>().ReverseMap().MaxDepth(3);
            CreateMap<ValuationResultViewModel, ValuationResult>()
                .MaxDepth(3).ReverseMap();
            CreateMap<InstructionAppointmentModel, InstructionAppointmentModel>().ReverseMap();
            CreateMap<InstructionHistoryModel, InstructionHistory>().ReverseMap();
            CreateMap<OrganisationViewModel, Organisation>().ReverseMap();
            CreateMap<AccountViewModel, Account>().ReverseMap();
            CreateMap<BankAccountViewModel, BankAccount>().ReverseMap();
            CreateMap<AV.Contracts.Models.BankAccount, BankAccount>().ReverseMap();
            CreateMap<AV.Contracts.Models.Account, Account>().ReverseMap();
            CreateMap<PhoneViewModel, Phone>().ReverseMap();
            CreateMap<UserRolesViewModel, UserRole>().ReverseMap();
            CreateMap<ComparableResultComparable, ReportComparablesViewModel>();
            CreateMap<ComparableBandSizeViewModel, ComparableBandSize>().ReverseMap();
            CreateMap<ComparableViewModel, Comparable>().ReverseMap().PreserveReferences().MaxDepth(3);
            CreateMap<ComparableResultViewModel, ReportRequest>().ReverseMap();
            CreateMap<ComparableResultViewModel, ComparableResult>().ReverseMap();
            CreateMap<LandRateViewModel, LandRate>().ReverseMap();
            CreateMap<BuildingCostViewModel, BuildingCost>().ReverseMap();
            CreateMap<CreateInstantReportCommand, Comparable>();
            CreateMap<ComparableResult, InstantReportViewModel>()
                .ForMember(dest => dest.PropertyInfor, opt => opt.MapFrom(src => src.Comparable))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ComparableRequestViewModel, Comparable>();
            CreateMap<ComparableRequestViewModel, ComparableRequestViewModel>();
            CreateMap<ComparableRequestViewModel, Comparable>()
                .ForMember(dest => dest.LocalityId
                    , opts => opts.MapFrom(src => src.LocalityId))
                .ForMember(dest => dest.DateOfSale
                    , opts => opts.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.LandUse
                    , opts => opts.MapFrom(src => src.LandUse))
                .ForMember(dest => dest.PlotId
                    , opts => opts.MapFrom(src => src.PlotId))
                .ForMember(dest => dest.PlotNo
                    , opts => opts.MapFrom(src => src.PlotNo))
                .ForMember(dest => dest.PropertyType
                    , opts => opts.MapFrom(src => src.PropertyType))
                .ForMember(dest => dest.StreetName
                    , opts => opts.MapFrom(src => src.StreetName))
                .ForMember(dest => dest.StreetId
                    , opts => opts.MapFrom(src => src.StreetId))
                .ForMember(dest => dest.LocationId
                    , opts => opts.MapFrom(src => src.LocationId))
                .ForMember(dest => dest.PlotSize
                    , opts => opts.MapFrom(src => src.Size))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}