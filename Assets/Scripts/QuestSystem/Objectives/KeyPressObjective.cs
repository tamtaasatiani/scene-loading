using System;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Objective/Key Press")]
    public class KeyPressObjective : Objective
    {
        private int _completedAmount = 0;
        
        [SerializeField] private KeyCode key;
        [SerializeField] private int amount;
        
        public KeyCode Key { get => key; private set => key = value; }
        
        public override void StartObjective()
        {
            _completedAmount = 0;
            base.StartObjective();
        }

        public override void UpdateObjective()
        {
            if (!_isActive) return;
            if (IsCompleted) return;
            
            _completedAmount++;

            if (_completedAmount >= amount)
            {
                Complete();
            }

            base.UpdateObjective();
        }
    }
}