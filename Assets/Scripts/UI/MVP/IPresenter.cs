using System;
using UnityEngine;

namespace UI.MVP
{
    public interface IPresenter<TView> : IDisposable where TView : IView
    {
        void DestroyView(TView view);
    }
}
