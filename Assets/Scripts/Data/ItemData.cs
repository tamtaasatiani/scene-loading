using UnityEngine;

namespace Data
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        
        public string Name => itemName;
    }
}
