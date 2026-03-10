using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    public class ObjectiveManager : SingletonMonoBehaviour<ObjectiveManager>
    {
        private List<Objective> _activeObjectives = new List<Objective>();
        
        [SerializeField] private ObjectiveLibrary objectives;

        private void OnEnable()
        {
            objectives.SubscribeToObjectiveStarted(AddToActiveObjectives);
        }

        private void OnDisable()
        {
            objectives.UnsubscribeToObjectiveStarted(AddToActiveObjectives);
        }

        private void AddToActiveObjectives(Objective objective)
        {
            _activeObjectives.Add(objective);
        }
        
        public Objective FindActiveByName(string objName)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.Name == objName);
            return result;
        }
        
        public Objective FindByName(string objName)
        {
            var result = objectives.FindByName(objName);
            return result;
        }

        public void AddListener(string objName, Action<Objective> @event)
        {
            if (objectives == null)
            {
                Debug.LogError($"No objective library provided: {objectives}");
                return;
            }
            
            var objective = FindByName(objName);
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

            var objective = FindByName(objName);
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
            
            var objective = FindActiveByName(objName);
            if (objective == null)
            {
                Debug.LogWarning($"No active objective with name: {objName}");
                return;
            }
            
            objective.UpdateObjective();
        }
    }
}
