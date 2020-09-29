using MouldTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool.Views
{
   /// <summary>
   /// ShapePlate.xaml 的交互逻辑
   /// </summary>
   public partial class ShapePlate : UserControl
   {
      private ObservableCollection<ShapeItem> shapeItems;

      public object ShapeItems { get => (ObservableCollection<ShapeItem>)GetValue(MyPropertyProperty); set => SetValue(MyPropertyProperty, value); }
      public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.Register("ShapeItems", typeof(object), typeof(ShapePlate), new PropertyMetadata(null, ShapeItemsCallback));

      public ShapePlate()
      {
         InitializeComponent();
      }

      private static void ShapeItemsCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var shapePlate = d as ShapePlate;
         shapePlate.shapeItems = e.NewValue as ObservableCollection<ShapeItem>;
         shapePlate.shapeItems.CollectionChanged += (s, a) =>
         {
            if (a.OldItems == null) shapePlate.AddItem(a.NewItems[0] as ShapeItem);
         };
      }

      private void AddItem(ShapeItem item)
      {
         this.EditItem(item);
         this.Plate.Children.Add(item.Shape);
         item.PropertyModified += Item_PropertyModified;
      }

      private void EditItem(ShapeItem item)
      {
         var x = Convert.ToInt32(item.Radius * Math.Cos(item.Theta * Math.PI / 180) + this.Plate.ActualWidth / 2 - item.Shape.Height / 2);
         var y = Convert.ToInt32(item.Radius * Math.Sin(item.Theta * Math.PI / 180) + this.Plate.ActualHeight / 2 - item.Shape.Width / 2);
         Canvas.SetLeft(item.Shape, y);
         Canvas.SetTop(item.Shape, x);

         item.Shape.RenderTransformOrigin = new Point(0.5, 0.5);
         TransformGroup transGroup = new TransformGroup ();
         transGroup.Children.Add(new RotateTransform(item.Rotate));
         item.Shape.RenderTransform = transGroup;
      }

      private void Item_PropertyModified(object sender, ShapeItem e)
      {
         this.EditItem(sender as ShapeItem);
      }
   }
}
