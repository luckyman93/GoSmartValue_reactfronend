using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GoSmartValue.Web.Areas.api
{
    public static class ApiConstants
    {
        public const string AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Identity.Application";

        public static class Routes
        {
            public static class Product
            {
                public const string AllProducts = "api/products";
                public const string CreateProduct = "api/products";
                public const string GetProductById = "api/products/{id}";
            }

            public static class Packages
            {
                public const string AllPackage = "api/packages";
                public const string CreatePackage = "api/packages";
                public const string CreatePackages = "api/packages/bulk";
                public const string GetPackageById = "api/packages/{id}";
            }
        }
    }
}
