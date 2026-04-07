using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QuestSystem;
using QuickEye.Utility;
using ServiceLocation;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    [CreateAssetMenu(menuName = "Quest System/UI/UI Manager")]
    public class QuestUIManager : ScriptableObject
    {
        [SerializeField] private UnityDictionary<QuestState, QuestConfig> questUIConfigs = new();
        
        private List<QuestUIElement?> _uiElements = new();
        
        public List<QuestUIElement?> UIElements => _uiElements;

        public void Subscribe()
        {
            _uiElements.Clear();
            IServiceLocator.Default.GetService<QuestManager>().AddListenerPokeAllAsync(ConfigureUIElements).Forget();
            IServiceLocator.Default.GetService<QuestManager>().BroadcastPokeAllAsync().Forget();
        }

        public void Unsubscribe()
        {
            IServiceLocator.Default.GetService<QuestManager>().RemoveListenerPokeAllAsync(ConfigureUIElements).Forget();
        }

        private void ConfigureUIElements(Quest quest)
        {
            var uiElement = questUIConfigs[quest.QuestState].Configure(quest);
            if (uiElement != null)
                _uiElements.Add(uiElement);
        }
    }
}
