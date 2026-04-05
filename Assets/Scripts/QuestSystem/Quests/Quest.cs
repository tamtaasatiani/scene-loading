using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest")]
    public class Quest : ScriptableObject, IUpdateable<Quest>, IStartable<Quest>, ICompletable<Quest>
    {
        [SerializeField] private string questName;
        [SerializeField] private Image icon;
        [SerializeField] private Objective[] objectives;
        [SerializeField] private Reward[] rewards;
        [SerializeField] private bool autoStart = false;

        private QuestState _questState = QuestState.Unlearned;

        public string QuestName => questName;
        public Image Icon => icon;
        public QuestState QuestState => _questState;

        public event Action<Quest> OnLearned;
        public event Action<Quest> OnStarted;
        public event Action<Quest> OnUpdated;
        public event Action<Quest> OnCompleted;
        public event Action<Quest> OnFailed;
        public event Action<Quest> OnCollected;
        
        public void SubscribeToObjectiveUpdated(Action<Objective> action)
        {
            _questState = QuestState.Unlearned;
            foreach (var objective in objectives)
                objective.OnUpdated += action;
            
            if (autoStart) CustomStart(this);
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

        public virtual void Learn(Quest quest)
        {
            if (_questState != QuestState.Unlearned) return;
            
            ChangeState(QuestState.Learned, OnLearned);
        }

        public void CustomStart(Quest obj)
        {
            if (_questState == QuestState.Started) return;

            foreach (var objective in objectives)
                objective.CustomStart(objective);
            ChangeState(QuestState.Started, OnStarted);
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
            if (_questState != QuestState.Started) return;
            
            OnUpdated?.Invoke(this);
        }

        public void Complete(Quest obj)
        {
            if (_questState != QuestState.Started) return;
            Debug.Log("Quest completed");
            
            ChangeState(QuestState.Completed, OnCompleted);
        }

        public void Fail(Quest obj)
        {
            if (_questState != QuestState.Failed) return;
            Debug.Log("Quest failed");
            
            ChangeState(QuestState.Failed, OnFailed);
        }

        public void Collect(Quest obj)
        {
            if (_questState != QuestState.Completed) return;
            
            ChangeState(QuestState.Collected, OnCollected);
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