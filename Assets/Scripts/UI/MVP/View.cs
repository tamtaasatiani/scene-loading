using UnityEngine;

namespace UI.MVP
{
    public class View : MonoBehaviour, IView
    {
        private IPresenter<IView> _presenter;
        private IModel _model;

        public void Initialize(IPresenter<IView> presenter, IModel model)
        {
            _presenter = presenter;
            _model = model;
        }
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
