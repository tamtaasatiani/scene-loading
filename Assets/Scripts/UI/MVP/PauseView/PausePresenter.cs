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
            
            _model = model as PauseModel;

            if (_model == null)
            {
                Debug.LogError($"PausePresenter: model is invalid, {this}");
                return;
            }
            
            _model.QuestUIManager.Subscribe();
            base.Initialize(model, canvas);
        }

        public void InitializeQuestsView(GameObject questDisplayPrefab, GameObject questsContainer)
        {
            var uiElements = _model.QuestUIManager.UIElements;

            foreach (var uiElement in uiElements)
            {
                var displayElement = Instantiate(questDisplayPrefab, questsContainer.transform).GetComponent<QuestDisplayElement>();
                displayElement.Initialize(uiElement);
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
            _model.QuestUIManager.Unsubscribe();
            base.Dispose();
        }
    }
}
