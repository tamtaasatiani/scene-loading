using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : Observer<QuestManager, Quest>
    {
        private List<Quest> _activeQuests = new List<Quest>();
        
        
        
        public void AddQuest(Quest quest)
        {
            _activeQuests.Add(quest);
        }
    }
}
