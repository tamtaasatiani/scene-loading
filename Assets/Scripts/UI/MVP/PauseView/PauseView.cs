using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP.Pause
{
    public class PauseView : View
    {
        [SerializeField] private Button close;
        

        public override void Initialize(IModel model, Action disposeAction)
        {
            close.onClick.AddListener(Dispose);
            base.Initialize(model, disposeAction);
        }

        public void OnDestroy()
        {
            close.onClick.RemoveAllListeners();
        }
    }
}
