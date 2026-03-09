using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject
    {
        private string _name;
        
        public string Name => _name;
        
        public bool IsCompleted { get; private set; }
        
        public event Action<Objective> OnObjectiveStarted;
        public event Action<Objective> OnObjectiveCompleted;
        public event Action<Objective> OnObjectiveUpdated;

        public virtual void Start()
        {
            OnObjectiveStarted?.Invoke(this);
        }

        protected virtual void UpdateObjective()
        {
            OnObjectiveUpdated?.Invoke(this);
        }

        protected virtual void Complete()
        {
            IsCompleted = true;
            OnObjectiveCompleted?.Invoke(this);
        }
    }
}