using AV.Contracts.Enums;
using System.Collections.Generic;

namespace AV.Contracts.Models
{
    public static class Constants
    {
        public static string FromAddress => $"donotreply@gosmartvalue.com";


        public const double OneAcreToMetersSquared = 4046.856422;
        public const int OneHectreToMetersSquared = 10000;

        //Paypal Buttons
        public static readonly string InstantReport = AccountType.InstantReport.ToString();
        public static readonly string DetailedReport = AccountType.DetailedReport.ToString();
        public static readonly string AccountSetup = AccountType.AccountSetup.ToString();
        public static string BasicAccount = AccountType.Standard.ToString();
        public static string MonthlyAccount = AccountType.ValuerMonthly.ToString();
        
        public static string YearlyAccount = AccountType.ValuerYearly.ToString();
        public static readonly string CorporateQuarterly = AccountType.CorporateQuarterly.ToString();
        public static readonly string CorporateYearly = AccountType.CorporateYearly.ToString();
        public static readonly string CorporateMonthly = AccountType.CorporateMonthly.ToString();

        // Supported File Upload MimeTypes
        public static IDictionary<string,string> SupportedMimeTypesDictionary => new Dictionary<string, string>
        {
            {"",""},
            {".jpg","image/jpeg, image/pjpeg"},
            {".jpeg","image/jpeg, image/pjpeg"},
            {".png","image/png"},
            {".gif","image/gif"},
            {".pdf","application/pdf"},
            {".doc","application/msword"},
            {".docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".ppt","application/mspowerpoint, application/powerpoint, application/vnd.ms-powerpoint, application/x-mspowerpoint"},
            {".pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".pps","application/mspowerpoint, application/vnd.ms-powerpoint"},
            {".odt","application/vnd.oasis.opendocument.text"},
            {".xls","application/excel, application/vnd.ms-excel, application/x-excel, application/x-msexcel"},
            {".xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"}
        };

        //landing
        public const string LandingPageUrlDefault = "~/index.html";
        public const string LandingPageUrlStandard = "~/index.html";
        public const string LandingPageUrlCorporate = "~/corporate";
        public const string LandingPageUrlValuer = "~/valuer";
        public const string LandingPageUrlSalesAgent = "~/salesagent";
        public const string LandingPageUrlAdmin = "~/admin";
        public const string LandingPageUrlAnalyst = "~/analyst";

        public static decimal GetMetricMultiplierToMeterSquared(Metric metric)
        {
            switch (metric)
            {
                case Metric.SquareMetres:
                    return 1;
                case Metric.Acres:
                    return (decimal)OneAcreToMetersSquared;
                case Metric.Hectres:
                    return OneHectreToMetersSquared;
                default:
                    return 0;
            }
        }

        public static class ContentMediaTypes 
        {
            public static string PDF { 
                get {
                        return SupportedMimeTypesDictionary[".pdf"];
                    }
            }
        }

        public static class SystemConfigurations
        {
            public const string InflationRate = "InflationRate";
        }

        public static class AccessPolicies
        {
            public const string DeveloperAccess = "DeveloperAccess";
            public const string AdminAccess = "DeveloperAccess";
            public const string Valuer = "ValuerAccess";
            public const string Corporate = "CorporateAccess";
            public const string InternalStaff = "InternalStaffAccess";
            public const string Admin = "Admin";
            public const string ManagerAccess = "ManagerAccess";
            public const string All = "All";
            public const string ValuerAccess = "ValuerAccess";
            public const string SalesAgentAccess = "SalesAgentAccess";
        }

        public class WebsiteRoutes
        {
            public class AnalystUrls
            {
                //deeds
                public const string Dashboard = "analyst";
                public const string ManageUser = "analyst/users";
                public const string Locations = "analyst/locations";
                public const string DeedsTransactionsPortal = "analyst/deeds/portal";
                public const string DeedsTransactions = "analyst/deeds/transactions";
                public const string DeedsTransactionCreate = "analyst/deeds/transactions/add";
                public const string DeedsTransactionView = "analyst/deeds/transactions/view";
                public const string DeedsTransactionEdit = "analyst/deeds/transactions/edit/{id}";
                public const string DeedsTransactionDelete = "analyst/deeds/transactions/delete/{id}";
                public const string VerifyTransaction = "analyst/deeds/transactions/verify/{id}";

                public class MarketInformation
                {
                    public const string LandRatesImport = "analyst/landrates/import";
                    public const string LandRates = "analyst/landrates";
                    public const string BuildingCosts = "analyst/buildingcosts";
                    public const string BuildingCostsImport = "analyst/buildingcostsimport";
                    public const string BuildingMaterialCostsImport = "analyst/buildingmaterialcosts/import";
                    public const string BuildingMaterialCosts = "analyst/buildingmaterialcosts";
                }
            }

            public class Payment
            {
                public const string RedirectUrl = "/payment/paymentsuccess";
                public const string RequestPayment = "/payment/pay";
                public const string BackUrl = "/payment/pay";
                public const string CreateTokenUrl = "https://secure1.sandbox.directpay.online/API/v6/";
                public const string PaymentPageUrl = "https://secure1.sandbox.directpay.online/payv2.php?ID=";
            }
        }

        public class ApiRoutes
        {
            public class Market
            {
                public const string GetLandRates = "api/market/landrates";
                public const string GetAveragePlotSize = "api/market/averageplotsize";
                public const string GetAverageSellingPrice = "api/market/averagesellingprice";
                public const string GetInflationRate = "api/market/inflationrate";
                public const string PostInflationRate = "api/market/inflationrate";
                public const string PostMarketInformation = "api/market";
            }

            public class Products
            {
                public const string AllProducts = "api/products";
                public const string GetProductById = "api/products/{id}";
            }
        }

        public class Currency
        {
            public class Botswana
            {
                public const string Code = "BWP";
                public const string Name = "Pula";
            }
        }
    }
}
