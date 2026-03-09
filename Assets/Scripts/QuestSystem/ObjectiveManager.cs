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

        public void Broadcast(string objName)
        {
            var objective = objectives.FindByName(objName);
            if (objective == null)
            {
                Debug.LogWarning($"Objective not found: {objName}");
                return;
            }
            
            objective.UpdateObjective();
        }
    }
}
