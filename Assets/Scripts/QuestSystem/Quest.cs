using System;
using UnityEngine;

namespace QuestSystem
{
    public class Quest : ScriptableObject
    {
        [SerializeField] private Objective[] objectives;
        
        private QuestState _questState = default(QuestState);

        public QuestState QuestState
        {
            get { return _questState; }
            private set { _questState = value; }
        }
        
        public static event Action<Quest> OnQuestStarted;
        public static event Action<Quest> OnQuestCompleted;

        private void Start()
        {
            OnQuestStarted?.Invoke(this);
        }

        private void Complete()
        {
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