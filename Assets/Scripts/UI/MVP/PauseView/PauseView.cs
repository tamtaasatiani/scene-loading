using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP.Pause
{
    public class PauseView : View
    {
        private PauseModel _model;
        
        [SerializeField] private Button close;

        public override void Initialize(IModel model, Action disposeAction)
        {
            _model = model as PauseModel;
            close.onClick.AddListener(() => disposeAction());
            base.Initialize(model, disposeAction);
        }

        public void OnDestroy()
        {
            _model.OnClose?.Invoke();
            close.onClick.RemoveAllListeners();
        }
    }
}
