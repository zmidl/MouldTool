using MouldTool.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MouldTool.ViewModels
{
   public class MainWindowViewmodel : ViewModel
   {
      public ObservableCollection<ShapeItem> ShapeItems { get; set; } = new ObservableCollection<ShapeItem>();

      public ObservableCollection<CircleItem> CircleItems { get; set; } = new ObservableCollection<CircleItem>();

      public ShapeItem CurrentShape { get; set; } = new ShapeItem(-1, Models.Type.Rectangle, 50, 50, 30, 0, new CircleItem(-1, 200));

      public CircleItem CurrentCircle { get; set; }

      public int CurrentRadius { get; set; }

      public RelayCommand AddShape => new RelayCommand(() =>
      {
         this.CurrentShape.CircleItem = this.CurrentCircle;
         this.ShapeItems.Add(new ShapeItem(this.CurrentShape));
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
      });

      public RelayCommand CircleSelectionChanged => new RelayCommand((o) => 
      {
         this.CurrentRadius = (o as CircleItem).Radius;
         this.RaiseProperty(nameof(this.CurrentRadius));
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
