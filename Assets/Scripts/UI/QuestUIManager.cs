using System.Collections.Generic;
using QuestSystem;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    public class QuestUIManager : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<QuestState, QuestConfig> questUIConfigs;
        
        private List<QuestUIElement> _uiElements;
    }
}
