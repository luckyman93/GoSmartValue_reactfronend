using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}