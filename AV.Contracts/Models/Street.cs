namespace AV.Contracts.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int LocalityId { get; set; }
        public bool Verified { get; set; }
        public virtual Locality Locality { get; set; }
    }
}