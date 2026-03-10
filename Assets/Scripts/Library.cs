using System.Collections.Generic;
using UnityEngine;

public class Library : ScriptableObject
{
    [SerializeField] protected List<ScriptableObject> items;
    
    public ScriptableObject FindByName(string objName)
    {
        //var result = objectives.FirstOrDefault(objective => objective.Name == objName);
        //return result;
        return null;
    }
}
