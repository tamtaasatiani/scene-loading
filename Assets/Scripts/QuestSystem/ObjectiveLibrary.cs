using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Library/Objective")]
    public class ObjectiveLibrary : ScriptableObject
    {
        private List<Objective> _activeObjectives;
        
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
            return _activeObjectives.FirstOrDefault(objective => objective.Name == objName);
        }

        private void OnDisable()
        {
            foreach (var objective in objectives)
            {
                objective.OnObjectiveStarted -= AddToActiveObjectives;
            }
        }
    }
}
