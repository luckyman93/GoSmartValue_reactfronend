using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class Plot
    {
        [Key]
        public Guid PlotId { get; set; }
        public string PlotNo { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public int PolygonId { get; set; }
        public virtual PlotShape polygon { get; set; }
        //verified
        public int Size { get; set; }
        public LandUse LandUse { get; set; }
        public void CreateShape(int sizeInMetresSquared)
        {
            var side = Math.Sqrt(sizeInMetresSquared);
            polygon = new PlotShape
            {
                Height = side,
                Width = side
            };
        }

        public static float CalculatePolygonArea(PointF[] points)
        {
            // Add the first point to the end.
            int num_points = points.Length;
            PointF[] pts = new PointF[num_points + 1];
            points.CopyTo(pts, 0);
            pts[num_points] = points[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return Math.Abs(area);
        }
    }
}