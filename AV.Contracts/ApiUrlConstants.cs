namespace AV.Contracts
{
    public static class ApiUrlConstants
    {
        public static class Accounts
        {
            public const string GetUserStatus = "api/accounts/{username}/status";
            public const string LoggedInUserDetailsGet = "api/accounts/user/current";
            public const string PUT_EditUserProfile = "api/accounts/user/current/editprofile";
            public const string AllUserRolesDetailsGet = "api/accounts/roles";
            public const string RegisterStandardUserPost = "api/accounts/register";
            public const string RegisterValuer = "api/accounts/register/valuer";
            public const string LoginPost = "api/accounts/login";
            public const string LogOutGet = "api/accounts/logout";
            public const string AccountConfirmation = "api/account/confirm";
            public const string PasswordReset = "api/account/reset-password";
            public const string ForgotPassword = "api/account/forgot-password";
            public const string GetValuers = "api/account/users/valuers";
            public const string GetAllUsers = "api/account/users";
        }

        public static class Payments
        {
            public const string Response = "api/payments/response";
        }

        public static class Basket
        {
            public const string GET_CurrentBasket = "api/baskets/current";
            public const string GET_CurrentUsersPaidBaskets = "api/baskets/current/all/paid";
            public const string DELETE_CurrentBasket = "api/baskets/current";
            public const string PUT_ConfirmCurrentBasket = "api/baskets/current/confirm";
            public const string PUT_GeneratePaymentToken = "api/baskets/current/payment-token";

            public static class Items
            {
                public const string POST_BasketItems = "api/baskets/current/items";
                public const string PUT_BasketItemByID = "api/baskets/current/items/{id}";
                public const string DELETE_BasketItems = "api/baskets/current/items/{id}";
            }
        }
        public static class CallBack
        {
            public const string POST_SUCCESS = "api/callback/success";
        }

        public static class Markets
        {
            public const string GET_landRates = "api/markets/rates/land";
            public const string GET_buildingMaterialCosts = "api/markets/rates/building-material-cost";
            public const string GET_buildingCosts = "api/markets/rates/building-cost";
            public const string GET_salesTrends = "api/markets/sales-trends";
        }
    }
}
