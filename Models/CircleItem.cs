using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool.Models
{
   public class CircleItem
   {
      public int Id { get; set; }

      public int Radius { get; set; } = 150;

      public Ellipse Circie { get; set; } = new Ellipse { StrokeDashArray=new System.Windows.Media.DoubleCollection {8,5 },Stroke=Brushes.White };
   }
}
