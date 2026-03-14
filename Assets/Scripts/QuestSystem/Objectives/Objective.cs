using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject, IUpdateable, IStartable, ICompletable
    {
        protected bool _isActive;
        [SerializeField] private string objectiveName;
        
        public string Name => objectiveName;
        
        public bool IsCompleted { get; private set; }
        
        public event Action<ScriptableObject> OnStarted;
        public event Action<ScriptableObject> OnCompleted;
        public event Action<ScriptableObject> OnUpdated;

        
        public virtual void CustomStart()
        {
            
            _isActive = true;
            IsCompleted = false;
            OnStarted?.Invoke(this);
        }
        
        public virtual void CustomUpdate()
        {
            if (!_isActive) return;
            if (IsCompleted) return;
            OnUpdated?.Invoke(this);
        }

        public virtual void Complete()
        {
            _isActive = false;
            IsCompleted = true;
            OnCompleted?.Invoke(this);
        }

        public void RemoveAllListeners()
        {
            OnUpdated = null;
        }
    }
}