using System;
using UnityEngine;

namespace QuestSystem
{
    public class ObjectiveManager : SingletonMonoBehaviour<ObjectiveManager>
    {
        [SerializeField] private ObjectiveLibrary objectives;

        public void AddListener(string objName, Action<Objective> @event)
        {
            if (objectives == null)
            {
                Debug.LogError($"No objective library provided: {objectives}");
                return;
            }
            
            var objective = objectives.FindByName(objName);
            if (objective == null)
            {
                Debug.Log($"No active objective with name: {objName}");
                return;
            }
            
            objective.OnObjectiveUpdated += @event;
        }

        public void RemoveListener(string objName, Action<Objective> @event)
        {
            if (objectives == null)
            {
                Debug.LogError($"No objective library provided: {objectives}");
                return;
            }

            var objective = objectives.FindByName(objName);
            if (objective == null)
            {
                Debug.Log($"No active objective with name: {objName}");
                return;
            }
            
            objective.OnObjectiveUpdated -= @event;
        }

        public void Broadcast(string objName)
        {
            if (objectives == null)
            {
                Debug.LogError($"No objective library provided: {objectives}");
                return;
            }
            
            var objective = objectives.FindByName(objName);
            if (objective == null)
            {
                Debug.LogWarning($"No active objective with name: {objName}");
                return;
            }
            
            objective.UpdateObjective();
        }
    }
}
