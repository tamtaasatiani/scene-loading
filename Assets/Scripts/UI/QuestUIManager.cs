using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QuestSystem;
using QuickEye.Utility;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    [CreateAssetMenu(menuName = "Quest System/UI/UI Manager")]
    public class QuestUIManager : ScriptableObject
    {
        [SerializeField] private UnityDictionary<QuestState, QuestConfig> questUIConfigs = new();
        
        private List<QuestUIElement?> _uiElements;
        
        public List<QuestUIElement?> UIElements => _uiElements;

        private void Subscribe()
        {
            QuestManager.Instance.AddListenerPokeAllAsync(ConfigureUIElements).Forget();
            QuestManager.Instance.BroadcastPokeAllAsync().Forget();
        }

        private void Unsubscribe()
        {
            QuestManager.Instance.RemoveListenerPokeAllAsync(ConfigureUIElements).Forget();
        }

        private void ConfigureUIElements(Quest quest)
        {
            //var uiElement = questUIConfigs[quest.QuestState].Configure(quest);
            //if (uiElement != null)
            //    _uiElements.Add(uiElement);
        }
    }
}
