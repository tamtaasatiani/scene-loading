using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UI.MVP.Pause
{
    [CreateAssetMenu(menuName = "UI/MVP/Pause/Presenter")]
    public class PausePresenter : Presenter<PauseView>
    {
        private PauseModel _model;

        public override void Initialize(IModel model, Canvas canvas)
        {
            base.Initialize(model, canvas);
            
            _model = model as PauseModel;

            if (_model == null)
            {
                Debug.LogError($"PausePresenter: model is invalid, {this}");
                return;
            }
            
            _model.QuestUIManager.Subscribe();
        }

        public List<QuestUIElement?> ReturnUIElements()
        {
            return _model.QuestUIManager.UIElements;
        }

        public void Close()
        {
            _model.OnClose?.Invoke();
            Dispose();
        }
        
        public override void Dispose()
        {
            _model.OnClose = null;
            _model.QuestUIManager.Unsubscribe();
            base.Dispose();
        }
    }
}
