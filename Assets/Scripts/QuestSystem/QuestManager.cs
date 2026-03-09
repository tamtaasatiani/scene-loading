using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : SingletonMonobehaviour<QuestManager>
    {
        private List<Quest> _activeQuests;
        
        [SerializeField] private List<Quest> quests;

        private void OnEnable()
        {
            
        }
    }
}
