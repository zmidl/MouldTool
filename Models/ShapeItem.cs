using Microsoft.Expression.Shapes;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool.Models
{
   public class ShapeItem : Notify
   {
      public event EventHandler<ShapeItem> PropertyModified;

      public int Index { get; set; }

      public Shape Shape { get; set; }

      public Type Type
      {
         get
         {
            if (this.Shape is System.Windows.Shapes.Rectangle) return Type.Rectangle;
            else if (this.Shape is Ellipse) return Type.Ellipse;
            else return Type.Triangle;
         }
         set
         {
            switch (value)
            {
               case Type.Rectangle:
               {
                  this.Shape = new Rectangle { Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
               case Type.Ellipse:
               {
                  this.Shape = new Ellipse { Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
               case Type.Triangle:
               {
                  this.Shape = new RegularPolygon { PointCount = 3, Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
               default: break;
            }
            this.Shape.RenderTransformOrigin = new Point(0.5, 0.5);
            TransformGroup transGroup = new TransformGroup();
            transGroup.Children.Add(new RotateTransform(0));
            this.Shape.RenderTransform = transGroup;
            this.RaiseProperty(nameof(this.Type));
         }
      }

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

      public int RadiusNo { get; set; }

      private int theta = 30;
      public int Theta
      {
         get => this.theta;
         set
         {
            this.theta = value;
            this.RaiseProperty(nameof(this.theta));
         }
      }

      public int Rotate
      {
         get => (this.Shape == null) ? 0 : Convert.ToInt32((((TransformGroup)this.Shape.RenderTransform).Children?[0] as RotateTransform).Angle);
         set
         {
            if (this.Shape != null)
            {
               (((TransformGroup)this.Shape.RenderTransform).Children[0] as RotateTransform).Angle = value;
               this.RaiseProperty(Rotate);
               this.PropertyModified?.Invoke(this, null);
            }
         }
      }

      public ShapeItem(int index, Type type, int width, int height, int radius, int angle, int rotate)
      {
         this.Type = type;
         this.Rotate = rotate;
         this.Width = width;
         this.Height = height;
         this.Radius = radius;
         this.Theta = angle;
         this.Index = index;
      }

      public ShapeItem(ShapeItem origin) => this.Copy(origin);

      public void Copy(ShapeItem origin)
      {
        
         this.Type = origin.Type;
         this.Rotate = origin.Rotate;
         this.Width = origin.Width;
         this.Height = origin.Height;
         this.Radius = origin.Radius;
         this.Theta = origin.Theta;
         this.Index = origin.Index;
      }
   }

   public enum Type
   {
      Rectangle = 0,
      Ellipse = 1,
      Triangle = 2
   }
}
