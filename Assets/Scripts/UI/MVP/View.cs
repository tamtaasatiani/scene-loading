using System;
using UnityEngine;

namespace UI.MVP
{
    public class View : MonoBehaviour, IView
    {
        protected IPresenter<IView> _presenter;

        public virtual void Initialize(IPresenter<IView> presenter)
        {
            _presenter = presenter;
        }
    }
}
