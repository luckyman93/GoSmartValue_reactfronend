namespace AV.Contracts.Enums
{
    public enum DataState
    {
        Unknown = -1,
        //Data that has just come in - 
        Raw = 0,
        //Data has been acknowledge and is being checked
        Pending = 1,
        //Can be used for comparables
        Verified = 2,
        //Rejected records
        Discarded = 3,
    }
}