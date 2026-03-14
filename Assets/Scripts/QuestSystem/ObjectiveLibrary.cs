using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Library/Objective")]
    public class ObjectiveLibrary : Library<Objective>
    {
        public void SubscribeToObjectiveStarted(Action<ScriptableObject> action)
        {
            foreach (var objective in items)
                objective.OnStarted += action;
        }

        public void UnsubscribeToObjectiveStarted(Action<ScriptableObject> action)
        {
            foreach (var objective in items)
                objective.OnStarted -= action;
        }
        
        public Objective FindByName(string objName)
        {
            var result = items.FirstOrDefault(objective => objective.Name == objName);
            return result;
        }
    }
}
