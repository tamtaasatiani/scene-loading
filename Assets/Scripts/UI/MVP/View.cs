using System;
using UnityEngine;

namespace UI.MVP
{
    public class View : MonoBehaviour, IView
    {
        private IPresenter<IView> _presenter;

        public virtual void Initialize(IPresenter<IView> presenter, Action disposeAction)
        {
            _presenter = presenter;
        }
    }
}
