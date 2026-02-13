using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.MVP
{
    public class Presenter<TView> : IPresenter<TView> where TView : IView
    {
        protected TView _view;
        private GameObject _obj;
        
        public Presenter(IModel model, Canvas canvas)
        {
            CreateViewAsync(model, canvas).Forget();
        }

        private async UniTask CreateViewAsync(IModel model, Canvas canvas)
        {
            //location: 
            _obj = await Addressables.InstantiateAsync(typeof(TView).Name);
            _view = _obj.GetComponent<TView>();
            var presenter = this as IPresenter<IView>;
            _obj.transform.parent = canvas.transform;
            _obj.transform.localPosition = new Vector3(0, 0, 0);
            _obj.transform.localRotation = Quaternion.identity;
            _obj.transform.localScale = Vector3.one;
            _view.Initialize(model);
        }

        public void DestroyView(TView view)
        {
            ReleaseViewAsync();
        }

        private UniTask ReleaseViewAsync()
        {
            Addressables.Release(_obj);
            return UniTask.CompletedTask;
        }
    }
}
