using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.MVC
{
    public abstract class Controller<TView, TModel> : IGUIController<TView, TModel>
        where TView : class, IView
        where TModel : class, IModel
    {
        public Controller() 
        { 
            Model = Activator.CreateInstance<TModel>();
        }

        public TView View { get; private set; }

        public TModel Model { get; }

        public abstract void UpdateView();

        protected abstract void ShowView();
        protected abstract void HideView();

        public void AddView<T>(T view)
            where T : class, IView
        {
            View = view as TView;
            View.OnHide += HideView;
            View.OnShow += ShowView;
        }

    }
}
