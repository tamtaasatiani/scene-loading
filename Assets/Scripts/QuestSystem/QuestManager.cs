using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : Observer<QuestManager, Quest>
    {
        private bool _questsInitialized = false;
        private List<Quest> _activeQuests = new List<Quest>();
        
        [SerializeField] private ObjectiveLibrary objectiveLibrary;

        private void OnEnable()
        {
            InitializeQuestsToObjectives();
        }
        
        private void InitializeQuestsToObjectives()
        {
            foreach (var quest in library.GetAll())
            {
                quest.SubscribeToObjectiveUpdated(quest.CustomUpdate);
                quest.SubscribeToObjectiveCompleted(quest.Complete);
            }
            
            _questsInitialized = true;
        }

        private void OnDisable()
        {
            foreach (var quest in library.GetAll())
            {
                quest.UnsubscribeFromObjectiveUpdated(quest.CustomUpdate);
                quest.UnsubscribeFromObjectiveCompleted(quest.Complete);
            }
        }

        public async UniTask AddListenerStart(int hashCode, Action<ScriptableObject> callback)
        {
            if (library == null)
            {
                Debug.LogError($"Observer {this.GetType()} cannot find element in empty library");
                return;
            }
        
            var item = library.FindByHash(hashCode);
        
            if (item == null)
            {
                Debug.LogError($"Observer {this.GetType()} cannot find element in library");
                return;
            }

            if (!_questsInitialized)
                await UniTask.WaitUntil(_questsInitialized, condition => condition);
                
            item.OnUpdated += callback;
        }
    }
}
