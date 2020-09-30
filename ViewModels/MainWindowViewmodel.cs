using MouldTool.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MouldTool.ViewModels
{
   public class MainWindowViewmodel : ViewModel
   {
      public ObservableCollection<ShapeItem> ShapeItems { get; set; } = new ObservableCollection<ShapeItem>();

      public ObservableCollection<CircleItem> CircleItems { get; set; } = new ObservableCollection<CircleItem>();

      public ShapeItem CurrentShape { get; set; } = new ShapeItem(-1, Models.Type.Rectangle, 50, 50, 30, 0, new CircleItem(-1, 200));

      public CircleItem CurrentCircle { get; set; }

      public int CurrentRadius { get; set; }

      public int PlateTheta { get; set; }

      public RelayCommand AddShape => new RelayCommand(() =>
      {
         if (this.CurrentCircle == null) MessageBox.Show("请指定半径");
         else
         {
            this.CurrentShape.CircleItem = this.CurrentCircle;
            this.CurrentShape.Index = this.CalcShapeId();
            var shapeItem = new ShapeItem(this.CurrentShape);
            this.ShapeItems.Add(shapeItem);
            shapeItem.Shape.MouseDown += (s, e) => this.SetHighlight(s);
         }
      });

      public RelayCommand Change => new RelayCommand(() =>
      {
         this.ShapeItems[0].Width = 100;
         this.ShapeItems[0].Height = 20;
         this.ShapeItems[0].Rotate = 45;
      });

      public RelayCommand AddCircle => new RelayCommand(() => this.CircleItems.Add(new CircleItem(this.CalcCircleId(), this.CurrentRadius)));

      public RelayCommand ShapeSelectionChanged => new RelayCommand((o) =>
      {
         var shapeItem = o as ShapeItem;
         this.CurrentShape.Copy(shapeItem);
         this.RaiseProperty(nameof(this.CurrentShape));
         this.CurrentCircle = this.CircleItems.FirstOrDefault(o2 => o2 == shapeItem.CircleItem);
         this.RaiseProperty(nameof(this.CurrentCircle));
         this.SetHighlight(shapeItem);
      });

      public RelayCommand CircleSelectionChanged => new RelayCommand((o) =>
      {
         this.CurrentRadius = (o as CircleItem).Radius;
         this.RaiseProperty(nameof(this.CurrentRadius));
      });

      public RelayCommand RotatePlate => new RelayCommand(() => 
      {
         this.ShapeItems.ToList().ForEach(o => o.Theta += this.PlateTheta);
      });

      public int SelectedShapeIndex { get; set; }

      private int CalcCircleId()
      {
         var id = this.CircleItems.Count;
         for (int i = 0; i < this.CircleItems.Count; i++)
         {
            if (id == this.CircleItems[i].Id) id++;
         }
         return id;
      }

      private int CalcShapeId()
      {
         var id = this.ShapeItems.Count;
         for (int i = 0; i < this.ShapeItems.Count; i++)
         {
            if (id == this.ShapeItems[i].Index) id++;
         }
         return id;
      }

      private void SetHighlight(object shapeItem)
      {
         this.ShapeItems.ToList().ForEach(o3 => o3.SetHighlight(false));
         if (shapeItem is ShapeItem item) item.SetHighlight(true);
         else
         {
            var shape = this.ShapeItems.FirstOrDefault(o => o.Shape == shapeItem);
            this.SelectedShapeIndex = this.ShapeItems.IndexOf(shape);
            this.RaiseProperty(nameof(this.SelectedShapeIndex));
            shape.SetHighlight(true);
         }
      }
   }
}
