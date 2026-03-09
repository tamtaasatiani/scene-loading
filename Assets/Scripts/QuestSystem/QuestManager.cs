using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : SingletonMonoBehaviour<QuestManager>
    {
        private List<Quest> _activeQuests;
        
        [SerializeField] private List<Quest> quests;

        private void OnEnable()
        {
            
        }
    }
}
