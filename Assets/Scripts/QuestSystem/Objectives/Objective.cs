using System;
using UnityEngine;

namespace QuestSystem
{
    //[CreateAssetMenu(menuName = "Quest System/Objective")]
    public class Objective : ScriptableObject
    {
        private string _name;
        
        public string Name => _name;
        
        public event Action<Objective> OnObjectiveStarted;
        public event Action<Objective> OnObjectiveCompleted;

        protected virtual void Start()
        {
            OnObjectiveStarted?.Invoke(this);
        }

        protected virtual void UpdateObjective()
        {
            throw new NotImplementedException();
        }

        protected virtual void Complete()
        {
            OnObjectiveCompleted?.Invoke(this);
        }
    }
}