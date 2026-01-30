using System;

namespace UI.MVP
{
    public interface IView : IDisposable
    {
        void Initialize(IPresenter<IView> presenter, IModel model);
    }
}
