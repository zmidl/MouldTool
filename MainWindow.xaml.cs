using MouldTool.Models;
using MouldTool.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MouldTool
{
   /// <summary>
   /// MainWindow.xaml 的交互逻辑
   /// </summary>
   public partial class MainWindow : Window
   {
      private ViewModel viewModel;

      public MainWindow()
      {
         InitializeComponent();
      }
   }
}
