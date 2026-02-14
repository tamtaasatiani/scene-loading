using System;
using UnityEngine;

namespace UI.MVP
{
    public class View : MonoBehaviour, IView
    {
        private IPresenter<IView> _presenter;
        private IModel _model;

        private Action OnDispose;

        public virtual void Initialize(IModel model, Action disposeAction)
        {
            _model = model;
            OnDispose += disposeAction;
        }
        
        public void Dispose()
        {
            _model = null;
            OnDispose?.Invoke();
            OnDispose = null;
            Destroy(gameObject);
        }
    }
}
