using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP.Pause
{
    public class PauseView : View
    {
        private PauseModel _model;
        
        [SerializeField] private GameObject questsView;
        
        [Header("Buttons")]
        [SerializeField] private Button close;
        [SerializeField] private Button quests;
        [SerializeField] private Button closeQuests;


        public override void Initialize(IPresenter<IView> presenter, Action disposeAction)
        {
            close.onClick.AddListener(() => disposeAction());
            quests.onClick.AddListener(OpenQuestsView);
            closeQuests.onClick.AddListener(CloseQuestsView);
            
            base.Initialize(presenter, disposeAction);
        }

        private void OpenQuestsView()
        {
            questsView.SetActive(true);
        }

        private void CloseQuestsView()
        {
            questsView.SetActive(false);
        }

        public void OnDestroy()
        {
            _model.OnClose?.Invoke();
            close.onClick.RemoveAllListeners();
            quests.onClick.RemoveAllListeners();
            closeQuests.onClick.RemoveAllListeners();
        }
    }
}
