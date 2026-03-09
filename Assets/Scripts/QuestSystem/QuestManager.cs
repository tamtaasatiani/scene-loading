using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : SingletonMonoBehaviour<QuestManager>
    {
        private List<Quest> _activeQuests = new List<Quest>();
        
        //[SerializeField] private List<Quest> quests;
        [SerializeField] private Quest testQuest;
        
        private void OnEnable()
        {
            testQuest.OnQuestStarted += AddQuest;
            testQuest.StartQuest();
        }
        
        private void OnDisable()
        {
            testQuest.OnQuestStarted -= AddQuest;
        }
        
        public void AddQuest(Quest quest)
        {
            _activeQuests.Add(quest);
        }
    }
}
