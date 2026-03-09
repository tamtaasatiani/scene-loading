using System;
using UnityEngine;

namespace QuestSystem
{
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
        
        public static event Action<Quest> OnQuestStarted;
        public static event Action<Quest> OnQuestCompleted;

        public void Start()
        {
            _questState = QuestState.Started;
            OnQuestStarted?.Invoke(this);
        }

        public void Complete()
        {
            _questState = QuestState.Completed;
            OnQuestCompleted?.Invoke(this);
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