using QuestSystem;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Quest System/UI/Config")]
    public class QuestConfig : ScriptableObject
    {
        [SerializeField] private bool visible;

        public QuestUIElement? Configure(Quest quest)
        {
            if (visible) return new QuestUIElement();

            return null;
        }
    }
}

