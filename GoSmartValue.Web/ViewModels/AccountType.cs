namespace GoSmartValue.Web.ViewModels
{
    public enum AccountType
    {
        Standard,       // the user ( typical individual who wants to know value of property)
        // Name Surname email address contact number sufficient for registration,
        // each search must be saved under profile. Registration is automatic
        // this user should be able to calculate estimated value for free,
        Valuer,         //independent to us but customizes results to produce reports for own client

        SalesAgent,     //an advisor to a seller or a buyer, who wants leads or is assisted
        //by the platform to reach a sales price they ca go to market with
        Corporate
        
                    
    }
}