using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Library/Objective")]
    public class ObjectiveLibrary : ScriptableObject
    {
        [SerializeField] private List<Objective> objectives;

        public Objective FindByName(string objName)
        {
            return objectives.FirstOrDefault(objective => objective.Name == objName);
        }
    }
}
