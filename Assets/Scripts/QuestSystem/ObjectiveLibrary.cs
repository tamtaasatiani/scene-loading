using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Library/Objective")]
    public class ObjectiveLibrary : ScriptableObject
    {
        private List<Objective> _activeObjectives = new List<Objective>();
        
        [SerializeField] private List<Objective> objectives;

        private void OnEnable()
        {
            foreach (var objective in objectives)
            {
                objective.OnObjectiveStarted += AddToActiveObjectives;
            }
        }

        private void AddToActiveObjectives(Objective objective)
        {
            _activeObjectives.Add(objective);
        }
        
        public Objective FindByName(string objName)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.Name == objName);
            return result;
        }

        private void OnDisable()
        {
            foreach (var objective in objectives)
                objective.OnObjectiveStarted -= AddToActiveObjectives;
        }
    }
}
