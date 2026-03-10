using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    public class ObjectiveManager : Observer<ObjectiveManager, Objective>
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

        private void AddToActiveObjectives(ScriptableObject objective)
        {
            var obj = objective as Objective;
            if (obj == null)
            {
                Debug.LogError($"Provided scriptableobject is not an objective, in {this}");
                return;
            }
            
            _activeObjectives.Add(obj);
        }
        
        public Objective FindActiveByName(string objName)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.Name == objName);
            return result;
        }

        private Objective FindActiveByHash(int hashCode)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.GetHashCode() == hashCode);
            return result;
        }
        
        public Objective FindByName(string objName)
        {
            var result = objectives.FindByName(objName);
            return result;
        }
        
        public override void Broadcast(int hashCode, Action callback = null)
        {
            if (objectives == null)
            {
                Debug.LogError($"No objective library provided: {objectives}");
                return;
            }
            
            var objective = FindActiveByHash(hashCode);
            if (objective == null)
            {
                Debug.LogWarning("No such active objective");
                return;
            }
            
            objective.CustomUpdate();
        }
    }
}
