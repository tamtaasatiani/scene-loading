using UnityEngine;

namespace UI.MVP
{
    public interface IPresenter<TView> where TView : IView
    {
        void DestroyView(TView view);
    }
}
