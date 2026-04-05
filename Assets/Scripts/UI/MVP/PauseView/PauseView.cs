using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP.Pause
{
    public class PauseView : View
    {
        private PausePresenter _pausePresenter;
        
        [SerializeField] private GameObject questsView;
        [SerializeField] private QuestUIManager questUIManager;
        
        [Header("Buttons")]
        [SerializeField] private Button close;
        [SerializeField] private Button quests;
        [SerializeField] private Button closeQuests;


        public override void Initialize(IPresenter<IView> presenter)
        {
            _pausePresenter = presenter as PausePresenter;

            if (_pausePresenter == null)
            {
                Debug.LogError("Invalid cast");
                return;
            }
            
            close.onClick.AddListener(Close);
            quests.onClick.AddListener(OpenQuestsView);
            closeQuests.onClick.AddListener(CloseQuestsView);
            
            base.Initialize(presenter);
        }

        private void OpenQuestsView()
        {
            questsView.SetActive(true);
        }

        private void CloseQuestsView()
        {
            questsView.SetActive(false);
        }

        private void Close()
        {
            _pausePresenter.Close();
        }

        public void OnDestroy()
        {
            close.onClick.RemoveAllListeners();
            quests.onClick.RemoveAllListeners();
            closeQuests.onClick.RemoveAllListeners();
        }
    }
}
