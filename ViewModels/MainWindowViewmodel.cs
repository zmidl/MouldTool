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
      public ObservableCollection<ShapeItem> ShapeItems { get; set; } = new ObservableCollection<ShapeItem>();

      public ObservableCollection<CircleItem> CircleItems { get; set; } = new ObservableCollection<CircleItem>();

      public ShapeItem CurrentShape { get; set; } = new ShapeItem(-1, Models.Type.Rectangle, 50, 50, 200, 30, 0);

      public CircleItem CurrentCircle { get; set; } = new CircleItem(-1, 400);

      public RelayCommand AddShape => new RelayCommand(() =>
      {
         this.CurrentShape.Radius = this.CurrentCircle.Radius/2;
         this.ShapeItems.Add(new ShapeItem(this.CurrentShape));
      });

      public RelayCommand Change => new RelayCommand(() =>
      {
         this.ShapeItems[0].Width = 100;
         this.ShapeItems[0].Height = 20;
         this.ShapeItems[0].Rotate = 45;
      });

      public RelayCommand AddCircle => new RelayCommand(() => this.CircleItems.Add(new CircleItem(this.CalcCircleId(), this.CurrentCircle.Radius)));

      public RelayCommand ShapeSelectionChanged => new RelayCommand((o) => 
      {
         var shapeItem = o as ShapeItem;
         this.CurrentShape.Copy(shapeItem);
         this.RaiseProperty(nameof(this.CurrentShape));
      });

      public RelayCommand CircleSelectionChanged => new RelayCommand((o) => 
      {
         this.CurrentCircle.Copy(o as CircleItem);
         this.RaiseProperty(nameof(this.CurrentCircle));
      });

      private int CalcCircleId()
      {
         var id = this.CircleItems.Count;
         for (int i = 0; i < this.CircleItems.Count; i++)
         {
            if (id == this.CircleItems[i].Id) id++;
         }
         return id;
      }
   }
}
