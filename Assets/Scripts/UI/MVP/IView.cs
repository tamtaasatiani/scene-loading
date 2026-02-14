using System;
using System.Collections;
using System.Collections.Generic;

namespace UI.MVP
{
    public interface IView : IDisposable
    {
        void Initialize(IModel model, Action DisposeAction);
    }
}
