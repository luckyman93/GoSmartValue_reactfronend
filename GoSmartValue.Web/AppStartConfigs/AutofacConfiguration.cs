using Autofac;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.Services;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Interface;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Market.Commands;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using AV.Contracts.Models.Payment.Commands;
using AV.Contracts.Models.Payment.Models;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Payment.Responses;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using AV.Contracts.Models.Valuation.Commands;
using AV.Contracts.Models.Valuation.ResponseModels;
using AV.Contracts.Models.ViewModels;
using AV.Contracts.Services;
using AV.Handlers.Accounts;
using AV.Handlers.Market.Command;
using AV.Handlers.Market.Queries;
using AV.Handlers.Market.Validators;
using AV.Handlers.Payment;
using AV.Handlers.Product;
using AV.Handlers.Reports;
using AV.Handlers.Valuation.Command;
using AV.Handlers.Valuation.Validators;
using AV.Infrastructure.Services;
using AV.Persistence.EntityFramework;
using AV.Persistence.EntityFramework.Helpers;
using AV.Persistence.EntityFramework.Queries;
using AV.Persistence.EntityFramework.Repositories;
using AV.Persistence.EntityFramework.Stores;
using AV.Persistence.EntityFramework.UnitOfWorks;
using AV.Persistence.EntityFramework.Valuations;
using AV.Persistence.EntityFramework.Valuations.Handlers;
using AV.Persistence.Queries;
using AV.Persistence.Stores;
using AV.Persistence.Stores.Valuations;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AV.Handlers.Basket.Services;
using AV.Infrastructure.Services.PaymentGateways;
using AV.Infrastructure.Services.PaymentGateways.Cellulant;
using Microsoft.AspNetCore.Http;
using BankAccount = AV.Contracts.Models.BankAccount;
using IUserManagerService = GoSmartValue.Web.Services.IUserManagerService;
using MarketInformation = AV.Contracts.Models.Market.ResponseModels.MarketInformation;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.AppStartConfigs
{
    public static class AutofacConfiguration
    {
        public static ContainerBuilder BuildApplicationContainer(this ContainerBuilder builder,
            IConfiguration configuration)
        {
            // Register Types
            AV.Contracts.AssemblyLoader.Load();
            AV.Handlers.AssemblyLoader.Load();
            AV.Infrastructure.Services.AssemblyLoader.Load();
            AV.Persistence.AssemblyLoader.Load();
            AV.Persistence.EntityFramework.AssemblyLoader.Load();

            builder.RegisterAssemblyTypes(GetSolutionAssemblies())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(ctx =>
                {
                    var config = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new AutoValuationProfile());
                    });
                    return config.CreateMapper();
                }).As<IMapper>()
                .InstancePerLifetimeScope();

            //Register MVC Controllers
            builder.RegisterTypes(typeof(Controller)).InstancePerLifetimeScope();
            builder.RegisterType<ValuationsContext>().As<IdentityDbContext<User, Role, Guid>>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UserManager<User>)).InstancePerLifetimeScope();
            builder.RegisterTypes(typeof(RoleManager<Role>)).InstancePerLifetimeScope();

            //Mapper
            builder.RegisterType<AutoValuationProfile>().As<Profile>().InstancePerLifetimeScope();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            //repositories
            ConfigureRepositories(builder);

            //units of work
            builder.RegisterType<LocationUnitOfWork>().As<ILocationUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UserManagerUnitOfWork>().As<IUserManagerUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ValuationsUnitOfWork>().As<IValuationsUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<InstructionUoW>().As<IInstructionUoW>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ComparableUnitOfWork>().As<IComparableUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<OrganisationUnitOfWork>().As<IOrganisationUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<DocumentStoreUoW>().As<IDocumentStoreUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<SubscriptionOptionQueries>().As<ISubscriptionOptionQueries>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<MarketInformationUoW>().As<IMarketInformationUoW>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ComparablesQueries>().As<IComparablesQueries>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ComparableStore>().As<IComparableStore>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(StoreBase<>)).As(typeof(IStore<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(QueriesBase<>)).As(typeof(IQueries<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DbSet<>)).InstancePerLifetimeScope();
            //Builders
            builder.RegisterType<ValuationBuilder>().InstancePerDependency();

            //factories
            RegisterServices(builder);

            RegisterHandlers(builder);

            // Validators
            RegisterValidators(builder);

            // request & notification handlers
            builder.Register<ServiceFactory>(ctxt =>
            {
                var c = ctxt.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            return builder;

        }

        private static void RegisterValidators(ContainerBuilder builder)
        {

            builder.RegisterTypes(typeof(LandRateValidator)).InstancePerLifetimeScope();
            builder.RegisterTypes(typeof(InstantValuationValidators)).InstancePerLifetimeScope();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //services
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<ValuationsService>().As<IValuationsService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<InstructionService>().As<IInstructionService>().InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.RegisterType<UserManagerService>().As<IUserManagerService>().InstancePerLifetimeScope()
                .PropertiesAutowired();            
            builder.RegisterType<OrderNumberService>().As<IOrderNumberService>().SingleInstance();

            builder.RegisterType<DocumentStoreService>().As<IDocumentStoreService>().InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.RegisterType<UserNotificationService>().As<IUserNotificationService>().InstancePerLifetimeScope();
            builder.RegisterType<ExceptionLogger>().As<IExceptionLogger>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<DocumentService>().As<IDocumentService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<MarketInformationService>().As<IMarketInformationService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UserManagerService>().InstancePerLifetimeScope().PropertiesAutowired();
            
            // ===> set flag to false to use Cellulant  <===
            var useDpo = false;
            if(useDpo)
                builder.RegisterType<DpoPaymentGateway>().As<IPaymentGatewayService>().InstancePerLifetimeScope();
            else
                builder.RegisterType<CellulantPaymentGateway>().As<IPaymentGatewayService>().SingleInstance();
            
            builder.RegisterType<CellulantEncryptionService>().SingleInstance();


            builder.RegisterType<CachedStorageService>().SingleInstance();
            builder.RegisterType<MailJetEmailService>().As<IEmailService>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<GenerateComparableHandler>().InstancePerLifetimeScope();
            builder.RegisterType<EstimateComparableHandler>().InstancePerLifetimeScope();
            builder.RegisterType<ValidateValuationHandler>().InstancePerLifetimeScope();
            builder.RegisterType<AutoAdjustValuationHandler>().InstancePerLifetimeScope();

            // Mediatr - Query Handlers
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<GetLandRatesHandler>().As<IRequestHandler<GetLandRatesRequest, MarketInformation>>();
            builder.RegisterType<GetAverageSellingPriceHandler>().As<IRequestHandler<GetAverageSellingPriceRequest, GetAverageSellingPriceResponse>>();
            builder.RegisterType<GetAveragePlotSizeHandler>().As<IRequestHandler<GetAveragePlotSizeRequest, GetAveragePlotSizeResponse>>();
            builder.RegisterType<GetInflationRateHandler>().As<IRequestHandler<GetInflationRateRequest, InflationRateResponse>>();
            builder.RegisterType<GetAllMarketLocationsHandler>().As<IRequestHandler<GetAllMarketLocationsRequest, IEnumerable<MarketInformation>>>();
            builder.RegisterType<GetAllCurrentLandRatesRequestHandler>().As<IRequestHandler<GetAllCurrentLandRatesRequest, ImportLandRatesResponse>>();
            builder.RegisterType<GetCurrentBuildingCostsRequestHandler>().As<IRequestHandler<GetCurrentBuildingCostsRequest, ImportBuildingCostsResponse>>();
            builder.RegisterType<GetProductDetailsRequestHandler>().As<IRequestHandler<GetProductDetailsRequest, ProductModel>>();
            builder.RegisterType<SetProductAmountRequestHandler>().As<IRequestHandler<SetProductAmountRequest, SetProductAmountResponse>>();
            builder.RegisterType<GetValueTrendReportRequestHandler>().As<IRequestHandler<GetValueTrendReportRequest, ValueTrendReportResponse>>();
            builder.RegisterType<InstantReportRequestHandler>().As<IRequestHandler<InstantReportRequest, PaymentTrack>>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<GetLocationsTransactionsRequestHandler>().As<IRequestHandler<GetLocationsTransactionsRequest, LocationsTransactionsResponse>>();
            builder.RegisterType<GetCurrentBuildingMaterialCostsRequestHandler>().As<IRequestHandler<GetCurrentBuildingMaterialCostsRequest, ImportBuildingMaterialCostsResponse>>();
            builder.RegisterType<GetSalesTrendRequestHandler>().As<IRequestHandler<SalesTrendRequest, SalesTrendResponse>>();


            // Mediatr Command Handlers
            builder.RegisterType<AddBankDetailsCommandHandler>().As<IRequestHandler<AddBankDetailsCommand, BankAccount>>();
            builder.RegisterType<ImportBuildingCostsCommandHandler>()
                .As<IRequestHandler<ImportBuildingCostsCommand, ImportBuildingCostsResponse>>();
            builder.RegisterType<AddProfileDetailsCommandHandler>()
                .As<IRequestHandler<AddProfileDetailsCommand, AV.Contracts.Models.Account>>();
            builder.RegisterType<AddMarketLocationCommandHandler>()
                .As<IRequestHandler<AddMarketInformationCommand, MarketInformation>>();
            builder.RegisterType<ImportLandRatesCommandHandler>()
                .As<IRequestHandler<ImportLandRatesCommand, ImportLandRatesResponse>>();
            builder.RegisterType<MakePaymentRequestHandler>()
                  .As<IRequestHandler<MakePaymentRequest, CreatePaymentTokenResponse>>()
                .InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<SetPaymentToPaidCommandHandler>().As<IRequestHandler<SetPaymentToPaidCommand, PaymentModel>>()
                .InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<SetPackageCommandHandler>().As<IRequestHandler<SetPackageCommand, PaymentTrack>>()
                .InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<GetManageAccountDataRequestHandler>()
                .As<IRequestHandler<GetManageAccountDataRequest, ManageAccountViewModel>>();
            builder.RegisterType<RenewAccountSubscriptionCommandHandler>()
                .As<IRequestHandler<RenewAccountSubscriptionCommand, RenewAccountSubscriptionResponse>>();
            builder.RegisterType<ImportInstantValuationCommandHandler>()
                .As<IRequestHandler<ImportInstantValuationCommand, ImportForInstantValuationResponse>>();
            builder.RegisterType<ImportBuildingMaterialCostsCommandHandler>()
                .As<IRequestHandler<ImportBuildingMaterialCostsCommand, ImportBuildingMaterialCostsResponse>>();
        }

        public static void ConfigureRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<AccountsRepository>().As<IAccountsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<BankAccountsRepository>().As<IBankAccountsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<LocalAreaRepository>().As<ILocalAreaRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<LocationsRepository>().As<ILocationsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<StreetsRepository>().As<IStreetsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UserManagerRepository>().As<IUserManagerRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ValuationsRepository>().As<IValuationsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<InstructionRepository>().As<IInstructionRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ComparableRepository>().As<IComparableRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<OrganisationRepository>().As<IOrganisationRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<DocumentRepository>().As<IDocumentRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<MarketInformationRepository>().As<IMarketInformationRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<PaymentsRepository>().As<IPaymentsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<BasketsQueries>().As<IBasketsQueries>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        }
        public static Assembly[] GetSolutionAssemblies()
        {
            var assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };
            var referencedAssemblies = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("AV."))
                .Select(Assembly.Load);

            assemblies.AddRange(referencedAssemblies);

            return assemblies.ToArray();
        }
    }
}