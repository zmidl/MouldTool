using System;
using System.Drawing;
using System.Windows.Shapes;

namespace MouldTool.Models
{
   public class ShapeItem:Notify
   {
      public event EventHandler<ShapeItem> PropertyModified;
      public int Index { get; set; }

      public Shape Shape { get; set; }

      public int Width
      {
         get
         {
            if (this.Shape != null && double.IsNaN(this.Shape.Width) == false) return Convert.ToInt32(this.Shape.Width);
            else return 0;
         }
         set
         {
            if (this.Shape != null) this.Shape.Width = value;
            this.RaiseProperty(Width);
            this.PropertyModified?.Invoke(this, null);
         }
      }

      public int Height
      {
         get
         {
            if (this.Shape != null && double.IsNaN(this.Shape.Height) == false) return Convert.ToInt32(this.Shape.Height);
            else return 0;
         }
         set
         {
            if (this.Shape != null) this.Shape.Height = value;
            this.RaiseProperty(Height);
            this.PropertyModified?.Invoke(this, null);
         }
      }


      public int Radius { get; set; }

      public int Theta { get; set; }

      private int rotate = 0;
      public int Rotate
      {
         get => this.rotate;
         set
         {
            this.rotate = value;
            this.RaiseProperty(Rotate);
            this.PropertyModified?.Invoke(this, null);
         }
      }


      public ShapeItem(int index, Shape shape, int radius, int angle, int rotate)
      {
         this.Index = index;
         this.Shape = shape;
         this.Radius = radius;
         this.Theta = angle;
         this.Rotate = rotate;
      }
   }

   public enum Type
   {
      Rectangle = 0,
      Ellipse = 1,
      Triangle = 2
   }
}
