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

        private void OnEnable()
        {
            foreach (var objective in objectives) 
                objective.OnCompleted += TryCompleteQuest;
        }

        public void CustomStart()
        {
            _questState = QuestState.Started;

            foreach (var objective in objectives)
                objective.CustomStart();
            OnStarted?.Invoke(this);
        }
        
        public void CustomUpdate()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            Debug.Log("Quest completed");
            _questState = QuestState.Completed;
            OnCompleted?.Invoke(this);
        }

        private void TryCompleteQuest(ScriptableObject objective)
        {
            bool completed = objectives.Where(obj => obj.IsCompleted == false).ToList().Count <= 0;
            if (!completed) return;
            
            Complete();
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