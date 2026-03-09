using System;
using UnityEngine;

namespace QuestSystem
{
    public class Objective : ScriptableObject
    {
        public static event Action<Objective> OnObjectiveStarted;
        public static event Action<Objective> OnObjectiveCompleted;

        protected virtual void Start()
        {
            OnObjectiveStarted?.Invoke(this);
        }

        protected virtual void Complete()
        {
            OnObjectiveCompleted?.Invoke(this);
        }
    }
}