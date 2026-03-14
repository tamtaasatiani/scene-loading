using System;
using System.Collections;
using System.Collections.Generic;

namespace UI.MVP
{
    public interface IView
    {
        void Initialize(IPresenter<IView> presenter, Action disposeAction);
    }
}
