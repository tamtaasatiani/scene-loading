using UnityEngine;

namespace UI.MVP.Pause
{
    public class PausePresenter : Presenter<PauseView>
    {
        public PausePresenter(IModel model, Canvas canvas) : base(model, canvas)
        {
            
        }
    }
}
