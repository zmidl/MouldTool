using MouldTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouldTool.ViewModels
{
   public abstract class ViewModel : Notify
   {
      public event EventHandler<object> ViewChanged;

      protected virtual void OnViewChanged(object obj = null)
      {
         this.ViewChanged?.Invoke(this, obj);
      }

      public void AddHandler(EventHandler<EventArgs> handler)
      {
         WeakEventManager<ViewModel, EventArgs>.AddHandler(this, nameof(this.ViewChanged), handler);
      }
   }
}
