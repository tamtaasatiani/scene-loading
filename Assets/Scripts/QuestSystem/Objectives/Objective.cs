using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject
    {
        protected bool _isActive;
        [SerializeField] private string objectiveName;
        
        public string Name => objectiveName;
        
        public bool IsCompleted { get; private set; }
        
        public event Action<Objective> OnObjectiveStarted;
        public event Action<Objective> OnObjectiveCompleted;
        public event Action<Objective> OnObjectiveUpdated;

        public virtual void StartObjective()
        {
            _isActive = true;
            IsCompleted = false;
            OnObjectiveStarted?.Invoke(this);
        }

        public virtual void UpdateObjective()
        {
            if (!_isActive) return;
            if (IsCompleted) return;
            OnObjectiveUpdated?.Invoke(this);
        }

        protected virtual void Complete()
        {
            _isActive = false;
            IsCompleted = true;
            OnObjectiveCompleted?.Invoke(this);
        }
    }
}