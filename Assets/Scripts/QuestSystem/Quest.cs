using System;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest")]
    public class Quest : ScriptableObject
    {
        [SerializeField] private Objective[] objectives;
        [SerializeField] private Reward[] rewards;
        
        private QuestState _questState = default(QuestState);

        public QuestState QuestState
        {
            get { return _questState; }
            private set { _questState = value; }
        }
        
        public event Action<Quest> OnQuestStarted;
        public event Action<Quest> OnQuestCompleted;

        public void StartQuest()
        {
            _questState = QuestState.Started;
            OnQuestStarted?.Invoke(this);

            foreach (var objective in objectives)
                objective.CustomStart();
        }

        private void OnEnable()
        {
            foreach (var objective in objectives) 
                objective.OnCompleted += TryCompleteQuest;
        }

        private void TryCompleteQuest(ScriptableObject objective)
        {
            bool completed = objectives.Where(obj => obj.IsCompleted == false).ToList().Count <= 0;
            if (!completed) return;
            
            Complete();
        }

        private void Complete()
        {
            Debug.Log("Quest completed");
            _questState = QuestState.Completed;
            OnQuestCompleted?.Invoke(this);
        }

        private void OnDisable()
        {
            foreach (var objective in objectives)
                objective.OnCompleted -= TryCompleteQuest;
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