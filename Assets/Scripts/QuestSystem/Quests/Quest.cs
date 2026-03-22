using System;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest")]
    public class Quest : ScriptableObject, IUpdateable, IStartable, ICompletable
    {
        [SerializeField] private Objective[] objectives;
        [SerializeField] private Reward[] rewards;
        
        private QuestState _questState = default(QuestState);

        public QuestState QuestState
        {
            get { return _questState; }
            private set { _questState = value; }
        }
        
        public event Action<ScriptableObject> OnStarted;
        public event Action<ScriptableObject> OnUpdated;
        public event Action<ScriptableObject> OnCompleted;
        
        public void SubscribeToObjectiveUpdated(Action<ScriptableObject> action)
        {
            foreach (var objective in objectives)
                objective.OnUpdated += CustomUpdate;
        }

        public void UnsubscribeFromObjectiveUpdated(Action<ScriptableObject> action)
        {
            foreach (var objective in objectives)
                objective.OnUpdated -= action;
        }
        
        public void SubscribeToObjectiveCompleted(Action<ScriptableObject> action)
        {
            foreach (var objective in objectives)
                objective.OnCompleted += action;
        }
        
        public void UnsubscribeFromObjectiveCompleted(Action<ScriptableObject> action)
        {
            foreach (var objective in objectives)
                objective.OnCompleted -= action;
        }

        public void CustomStart(ScriptableObject obj)
        {
            _questState = QuestState.Started;

            foreach (var objective in objectives)
                objective.CustomStart(objective);
            OnStarted?.Invoke(this);
        }
        
        public void CustomUpdate(ScriptableObject obj)
        {
            throw new NotImplementedException();
        }

        public void Complete(ScriptableObject obj)
        {
            Debug.Log("Quest completed");
            _questState = QuestState.Completed;
            OnCompleted?.Invoke(this);
        }

        private void TryCompleteQuest(ScriptableObject quest)
        {
            bool completed = objectives.Where(obj => obj.IsCompleted == false).ToList().Count <= 0;
            if (!completed) return;
            
            Complete(quest);
        }

        public void RemoveAllListeners()
        {
            OnStarted = null;
            OnUpdated = null;
            OnCompleted = null;
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