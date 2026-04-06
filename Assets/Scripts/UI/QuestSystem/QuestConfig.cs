using QuestSystem;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Quest System/UI/Config")]
    public class QuestConfig : ScriptableObject
    {
        [SerializeField] private bool visible;
        [SerializeField] private Sprite tick;

        public QuestUIElement? Configure(Quest quest)
        {
            if (!visible) return null;

            var element = new QuestUIElement
            {
                Icon = quest.Icon,
                Name = quest.QuestName,
                Tick = tick,
                Progressible = false
            };

            return element;
        }
    }
}

