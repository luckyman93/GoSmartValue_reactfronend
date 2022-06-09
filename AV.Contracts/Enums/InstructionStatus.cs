namespace AV.Contracts.Enums
{
    public enum InstructionStatus
    {
        New = 0,
        Pending = 1, // when valuer accepts
        Confirmed = 2, // when appointment is confirmed
        Processing = 3, // when valuation record is created
        Completed = 4,
        Rejected = 5
    }
}