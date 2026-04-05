using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QuestSystem;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    [CreateAssetMenu(menuName = "Quest System/UI/UI Manager")]
    public class QuestUIManager : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<QuestState, QuestConfig> questUIConfigs;
        
        private List<QuestUIElement?> _uiElements;
        
        public List<QuestUIElement?> UIElements => _uiElements;

        private void OnEnable()
        {
            QuestManager.Instance.AddListenerPokeAllAsync(ConfigureUIElements).Forget();
            QuestManager.Instance.BroadcastPokeAllAsync().Forget();
        }

        private void OnDisable()
        {
            QuestManager.Instance.RemoveListenerPokeAllAsync(ConfigureUIElements).Forget();
        }

        private void ConfigureUIElements(Quest quest)
        {
            var uiElement = questUIConfigs[quest.QuestState].Configure(quest);
            if (uiElement != null)
                _uiElements.Add(uiElement);
        }
    }
}
