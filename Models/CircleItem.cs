using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool.Models
{
   public class CircleItem:Notify
   {
      public int Id { get; set; }

      public int Radius
      {
         get => Convert.ToInt32(this.Circie.Width / 2);
         set
         {
            this.Circie.Width = value * 2;
            this.Circie.Height = value * 2;
            this.RaiseProperty(nameof(this.Radius));
         }
      }

      public Ellipse Circie { get; set; } = new Ellipse { StrokeDashArray = new DoubleCollection { 8, 5 }, Stroke = Brushes.White };

      public CircleItem(int id, int radius)
      {
         this.Id = id;
         this.Radius = radius;
      }

      public void Copy(CircleItem origin)
      {
         this.Id = origin.Id;
         this.Radius = origin.Radius;
      }
   }
}
