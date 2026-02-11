using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.MVP
{
    public class Presenter<TView> : IPresenter<TView> where TView : IView
    {
        private TView _view;
        
        public Presenter(IModel model)
        {
            CreateViewAsync(model).Forget();
        }

        private async UniTask CreateViewAsync(IModel model)
        {
            //location: 
            var obj = await Addressables.InstantiateAsync(typeof(TView).Name);
            _view = obj.GetComponent<TView>();
            var presenter = this as IPresenter<IView>;
            _view.Initialize(model);
        }
    }
}
