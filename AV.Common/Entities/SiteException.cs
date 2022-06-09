using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class SiteException : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}