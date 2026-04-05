using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable HeapView.CanAvoidClosure

namespace QuestSystem
{
    public class QuestManager : Observer<QuestManager, Quest>
    {
        private List<Quest> _activeQuests = new List<Quest>();
        
        [SerializeField] private ObjectiveLibrary objectiveLibrary;

        private void OnEnable()
        {
            InitializeQuestsToObjectives();
        }
        
        private void InitializeQuestsToObjectives()
        {
            //TODO: move objective initialization to own quest
            foreach (var quest in library.GetAll())
            {
                quest.SubscribeToObjectiveUpdated(quest.HandleUpdated);
                quest.SubscribeToObjectiveCompleted(quest.HandleCompleted);
            }

            _initialized = true;
        }

        private void OnDisable()
        {
            foreach (var quest in library.GetAll())
            {
                quest.UnsubscribeFromObjectiveUpdated(quest.HandleUpdated);
                quest.UnsubscribeFromObjectiveCompleted(quest.HandleCompleted);
            }
        }
        
        #region Poke Functions
        
        public override async UniTask AddListenerAsync(int hashCode, Action<Quest> callback)
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
            
            item.OnPoked += callback;
        }
        
        public override async UniTask RemoveListenerAsync(int hashCode, Action<Quest> callback)
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
            
            item.OnPoked -= callback;
        }
        
        public override async UniTask BroadcastAsync(int hashCode, Action callback = null)
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

            item.Poke(item);
            
            callback?.Invoke();
        }
        
        public async UniTask AddListenerPokeAllAsync(Action<Quest> callback)
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

            var quests = library.GetAll();

            foreach (var quest in quests)
                quest.OnPoked += callback;
        }
        
        public async UniTask RemoveListenerPokeAllAsync(Action<Quest> callback)
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

            var quests = library.GetAll();

            foreach (var quest in quests)
                quest.OnPoked -= callback;
        }
        
        public async UniTask BroadcastPokeAllAsync(Action callback = null)
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

            var quests = library.GetAll();

            foreach (var quest in quests)
                quest.Poke(quest);
            
            callback?.Invoke();
        }
        
        #endregion
        
        #region Add Listener

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
            
            item.OnLearned += callback;
        }
        
        public async UniTask AddListenerStartAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnStarted += callback;
        }
        public async UniTask AddListenerUpdateAsync(int hashCode, Action<ScriptableObject> callback)
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
        
        public async UniTask AddListenerCompleteAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnCompleted += callback;
        }
        
        #endregion
        
        #region Remove Listener

        public async UniTask RemoveListenerLearnAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnLearned -= callback;
        }
        
        public async UniTask RemoveListenerStartAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnStarted -= callback;
        }
        public async UniTask RemoveListenerUpdateAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnUpdated -= callback;
        }
        
        public async UniTask RemoveListenerCompleteAsync(int hashCode, Action<ScriptableObject> callback)
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
            
            item.OnCompleted -= callback;
        }
        
        #endregion
        
        #region Broadcasts

        public async UniTask BroadcastLearnAsync(int hashCode, Action callback = null)
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
            
            item.Learn(item);
            
            callback?.Invoke();
        }
        
        public async UniTask BroadcastStartAsync(int hashCode, Action callback = null)
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
            
            item.CustomStart(item);

            callback?.Invoke();
        }
        
        public async UniTask BroadcastUpdateAsync(int hashCode, Action callback = null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("This function is for debug purposes only. Typically, a quest is updated when one of its objectives is updated");
            
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
            
            item.CustomUpdate(item);
            
            callback?.Invoke();
#endif
        }
        
        public async UniTask BroadcastCompleteAsync(int hashCode, Action callback = null)
        {
            Debug.LogWarning("This function is for special cases. Typically, a quest completed when all of its objectives are completed");

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

            item.Complete(item);
            
            callback?.Invoke();
        }
        
        #endregion
    }
}
