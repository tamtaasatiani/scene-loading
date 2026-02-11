using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.MVP
{
    public class Presenter<TView> : IPresenter<TView> where TView : IView
    {
        private TView _view;
        
        public Presenter(TView view, IModel model)
        {
            _view = Addressables.LoadAssetAsync<TView>(typeof(TView).Name).Result;
            var presenter = this as IPresenter<IView>;
            _view.Initialize(model);
        }
    }
}
