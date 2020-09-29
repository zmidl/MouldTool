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
      public ObservableCollection<ShapeItem> ShapeItems { get => (ObservableCollection<ShapeItem>)GetValue(ShapeItemsProperty); set => SetValue(ShapeItemsProperty, value); }
      public static readonly DependencyProperty ShapeItemsProperty = DependencyProperty.Register(nameof(ShapeItems), typeof(ObservableCollection<ShapeItem>), typeof(ShapePlate), new PropertyMetadata(null, ShapeItemsCallback));

      public ObservableCollection<CircleItem> CircleItems { get => (ObservableCollection<CircleItem>)GetValue(CircleItemsProperty); set => SetValue(CircleItemsProperty, value); }
      public static readonly DependencyProperty CircleItemsProperty = DependencyProperty.Register(nameof(CircleItems), typeof(ObservableCollection<CircleItem>), typeof(ShapePlate), new PropertyMetadata(null, CircleItemsCallback));

      public ShapePlate()
      {
         InitializeComponent();
      }

      private static void ShapeItemsCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var shapePlate = d as ShapePlate;
         shapePlate.ShapeItems = e.NewValue as ObservableCollection<ShapeItem>;
         shapePlate.ShapeItems.CollectionChanged += (s, a) =>
         {
            if (a.OldItems == null) shapePlate.AddShape(a.NewItems[0] as ShapeItem);
         };
      }

      private static void CircleItemsCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var shapePlate = d as ShapePlate;
         shapePlate.CircleItems = e.NewValue as ObservableCollection<CircleItem>;
         shapePlate.CircleItems.CollectionChanged += (s, a) =>
         {
            if (a.OldItems == null) shapePlate.AddCircle(a.NewItems[0] as CircleItem);
         };
      }

      private void AddShape(ShapeItem item)
      {
         this.EditItem(item);
         this.Plate.Children.Add(item.Shape);
         item.PropertyModified += Item_PropertyModified;
      }

      private void AddCircle(CircleItem item)
      {
         var x = Convert.ToInt32(this.Plate.ActualWidth / 2 - item.Circie.Height / 2);
         var y = Convert.ToInt32(this.Plate.ActualHeight / 2 - item.Circie.Width / 2);
         Canvas.SetLeft(item.Circie, y);
         Canvas.SetTop(item.Circie, x);
         this.Plate.Children.Add(item.Circie);
         //item.PropertyModified += Item_PropertyModified;
      }

      private void EditItem(ShapeItem item)
      {
         var x = Convert.ToInt32(item.Radius * Math.Cos(item.Theta * Math.PI / 180) + this.Plate.ActualWidth / 2 - item.Shape.Height / 2);
         var y = Convert.ToInt32(item.Radius * Math.Sin(item.Theta * Math.PI / 180) + this.Plate.ActualHeight / 2 - item.Shape.Width / 2);
         Canvas.SetLeft(item.Shape, y);
         Canvas.SetTop(item.Shape, x);

         
         //item.Shape.RenderTransformOrigin = new Point(0.5, 0.5);
         //TransformGroup transGroup = new TransformGroup();
         //transGroup.Children.Add(new RotateTransform(item.Rotate));
         //item.Shape.RenderTransform = transGroup;
      }

      private void Item_PropertyModified(object sender, ShapeItem e)
      {
         this.EditItem(sender as ShapeItem);
      }
   }
}
