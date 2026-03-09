using System;
using UnityEngine;

namespace QuestSystem
{
    public class ObjectiveManager : SingletonMonoBehaviour<ObjectiveManager>
    {
        [SerializeField] private ObjectiveLibrary objectives;

        public void AddListener(string objName, Action<Objective> @event)
        {
            var objective = objectives.FindByName(objName);
            objective.OnObjectiveUpdated += @event;
        }

        public void RemoveListener(string objName, Action<Objective> @event)
        {
            var objective = objectives.FindByName(objName);
            objective.OnObjectiveUpdated -= @event;
        }
    }
}
