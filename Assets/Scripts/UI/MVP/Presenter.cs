using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.MVP
{
    public class Presenter<TView> : IPresenter<TView> where TView : IView
    {
        private TView _view;
        
        public Presenter(IModel model, Canvas canvas)
        {
            CreateViewAsync(model, canvas).Forget();
        }

        private async UniTask CreateViewAsync(IModel model, Canvas canvas)
        {
            //location: 
            var obj = await Addressables.InstantiateAsync(typeof(TView).Name);
            _view = obj.GetComponent<TView>();
            var presenter = this as IPresenter<IView>;
            obj.transform.parent = canvas.transform;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localRotation = Quaternion.identity;
            _view.Initialize(model);
        }
    }
}
