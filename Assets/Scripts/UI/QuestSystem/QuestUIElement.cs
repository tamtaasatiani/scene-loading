using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public struct QuestUIElement
    {
        public string Name { get; set; }
        public Sprite Icon { get; set; }
        public Sprite Tick { get; set; }
        public bool Progressible { get; set; }
        public float ProgressValue { get; set; }
    }
}
