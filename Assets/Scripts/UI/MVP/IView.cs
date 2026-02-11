using System;

namespace UI.MVP
{
    public interface IView : IDisposable
    {
        void Initialize(IModel model);
    }
}
