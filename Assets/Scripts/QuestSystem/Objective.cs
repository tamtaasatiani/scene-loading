using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject
    {
        private string _name;
        
        public string Name => _name;
        
        public event Action OnObjectiveStarted;
        public event Action OnObjectiveCompleted;

        protected virtual void Start()
        {
            OnObjectiveStarted?.Invoke();
        }

        protected virtual void Complete()
        {
            OnObjectiveCompleted?.Invoke();
        }
    }
}