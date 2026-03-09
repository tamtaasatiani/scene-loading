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

        protected override void UpdateObjective()
        {
            _completedAmount++;

            if (_completedAmount >= amount)
            {
                Complete();
            }

            base.UpdateObjective();
        }
    }
}