using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class PlotShape
    {
        [Key]
        public double ShapeId { get; set; }

        public double Height { get; set; }
        public double Width { get; set; }
    }
}