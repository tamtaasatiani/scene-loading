using System;
using UnityEditor;
using UnityEngine;

namespace UI.MVP.Pause
{
    public class PausePresenter : Presenter<PauseView>
    {
        private PauseModel _model;
        
        public PausePresenter(IModel model, Canvas canvas) : base(model, canvas)
        {
            _model = model as PauseModel;

            if (_model == null)
            {
                Debug.LogError($"PausePresenter: model is invalid, {this}");
                return;
            }
        }

        public void Close()
        {
            _model.OnClose?.Invoke();
            Dispose();
        }
        
        public override void Dispose()
        {
            _model.OnClose = null;
            base.Dispose();
        }
    }
}
