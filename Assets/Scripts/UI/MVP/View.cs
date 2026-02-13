using UnityEngine;

namespace UI.MVP
{
    public class View : MonoBehaviour, IView
    {
        private IPresenter<IView> _presenter;
        private IModel _model;

        public virtual void Initialize(IModel model)
        {
            _model = model;
        }
        
        public void Dispose()
        {
            _model = null;
            _presenter.DestroyView(this);
            Destroy(gameObject);
        }
    }
}
