using Microsoft.Expression.Shapes;
using MouldTool.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool.ViewModels
{
   public class MainWindowViewmodel : ViewModel
   {
      //public Action<Shape, int, int> AddItem { get; set; }
      public ObservableCollection<ShapeItem> ShapeItems { get; set; } = new ObservableCollection<ShapeItem>();
      public ObservableCollection<CircleItem> CircleItems { get; set; } = new ObservableCollection<CircleItem>();

      public ShapeItem Current { get; set; }

      public int ShapeIndex { get; set; } = 0;

      public int Theta { get; set; } = 30;

      public int Width { get; set; } = 50;

      public int Height { get; set; } = 50;

      public int Angle { get; set; } = 0;

      public RelayCommand AddShape => new RelayCommand(() =>
      {
         Shape shape = null;
         switch (this.ShapeIndex)
         {
            case 0:
               {
                  shape = new Rectangle { Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
            case 1:
               {
                  shape = new Ellipse { Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
            case 2:
               {
                  shape = new RegularPolygon {PointCount=3, Width = this.Width, Height = this.Height, Stroke = Brushes.White };
                  break;
               }
         }

         this.ShapeItems.Add(new ShapeItem(this.ShapeItems.Count, shape, 150, this.Theta, this.Angle));
      });

      public RelayCommand Change => new RelayCommand(() =>
      {
         this.ShapeItems[0].Width = 100;
         this.ShapeItems[0].Height = 20;
         this.ShapeItems[0].Rotate = 45;
      });
   }
}
