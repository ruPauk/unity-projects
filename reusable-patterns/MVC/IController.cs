using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.MVC
{
    public interface IController
    {
        public void AddView<T>(T view) where T : class, IView;
    }

    public interface IGUIController<TView, TModel> : IController
        where TView : class, IView
        where TModel : class, IModel
    {
        public TView View { get; }
        public TModel Model { get; }

        public void UpdateView();
    }
}


