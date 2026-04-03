using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject, IUpdateable<Objective>, IStartable<Objective>, ICompletable<Objective>
    {
        protected bool _isActive;
        [SerializeField] private string objectiveName;
        
        public string Name => objectiveName;
        
        public bool IsCompleted { get; private set; }
        
        public event Action<Objective> OnStarted;
        public event Action<Objective> OnCompleted;
        public event Action<Objective> OnUpdated;

        
        public virtual void CustomStart(Objective obj)
        {
            _isActive = true;
            IsCompleted = false;
            OnStarted?.Invoke(this);
        }
        
        public virtual void CustomUpdate(Objective obj)
        {
            OnUpdated?.Invoke(this);
        }

        public virtual void Complete(Objective obj)
        {
            _isActive = false;
            IsCompleted = true;
            OnCompleted?.Invoke(this);
        }

        public void RemoveAllListeners()
        {
            OnStarted = null;
            OnUpdated = null;
            OnCompleted = null;
        }
    }
}