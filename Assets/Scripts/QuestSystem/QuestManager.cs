using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable HeapView.CanAvoidClosure

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
        
        #region AddStateListeners

        public async UniTask AddListenerLearnAsync(int hashCode, Action<ScriptableObject> callback)
        {
            if (library == null)
            {
                Debug.LogError($"Observer {this.GetType()} cannot find element in empty library");
                return;
            }
        
            if (!_initialized)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                await UniTask.WaitUntil(() => _initialized);
            }

            var item = library.FindByHash(hashCode);
        
            if (item == null)
            {
                Debug.LogError($"Observer {this.GetType()} cannot find element in library");
                return;
            }
            
            item.OnUpdated += callback;
        }
        
        #endregion
    }
}
