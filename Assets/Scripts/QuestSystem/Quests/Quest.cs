using System;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest")]
    public class Quest : ScriptableObject, IUpdateable<Quest>, IStartable<Quest>, ICompletable<Quest>
    {
        [SerializeField] private Objective[] objectives;
        [SerializeField] private Reward[] rewards;
        
        private QuestState _questState = default(QuestState);

        public QuestState QuestState
        {
            get { return _questState; }
            private set { _questState = value; }
        }

        public event Action<Quest> OnLearned;
        public event Action<Quest> OnStarted;
        public event Action<Quest> OnUpdated;
        public event Action<Quest> OnCompleted;
        public event Action<Quest> OnFailed;
        public event Action<Quest> OnCollected;
        
        public void SubscribeToObjectiveUpdated(Action<Objective> action)
        {
            foreach (var objective in objectives)
                objective.OnUpdated += action;
        }

        public void UnsubscribeFromObjectiveUpdated(Action<Objective> action)
        {
            foreach (var objective in objectives)
                objective.OnUpdated -= action;
        }
        
        public void SubscribeToObjectiveCompleted(Action<Objective> action)
        {
            foreach (var objective in objectives)
                objective.OnCompleted += HandleCompleted;
        }
        
        public void UnsubscribeFromObjectiveCompleted(Action<Objective> action)
        {
            foreach (var objective in objectives)
                objective.OnCompleted -= HandleCompleted;
        }

        public void CustomStart(Quest obj)
        {
            _questState = QuestState.Started;

            foreach (var objective in objectives)
                objective.CustomStart(objective);
            OnStarted?.Invoke(this);
        }

        public void HandleUpdated(Objective obj)
        {
            CustomUpdate(this);
        }

        public void HandleCompleted(Objective obj)
        {
            TryCompleteQuest(this);
        }
        
        public void CustomUpdate(Quest obj)
        {
            throw new NotImplementedException();
        }

        public void Complete(Quest obj)
        {
            Debug.Log("Quest completed");
            _questState = QuestState.Completed;
            OnCompleted?.Invoke(this);
        }

        private void TryCompleteQuest(Quest quest)
        {
            bool completed = objectives.Where(obj => obj.IsCompleted == false).ToList().Count <= 0;
            if (!completed) return;
            
            Complete(quest);
        }

        private void ChangeState(QuestState state, Action<Quest> action)
        {
            _questState = state;
            action?.Invoke(this);
        }

        public void RemoveAllListeners()
        {
            OnLearned = null;
            OnStarted = null;
            OnUpdated = null;
            OnCompleted = null;
            OnFailed = null;
            OnCollected = null;
        }
    }

    public enum QuestState
    {
        Unlearned = 0,
        Learned,
        Started,
        Completed,
        Failed,
        Collected
    }
}