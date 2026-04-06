using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Library/Quest")]
    public class QuestLibrary : Library<Quest>
    {
        //public Quest FindByName(string objName)
        //{
        //    var result = items.FirstOrDefault(objective => objective.Name == objName);
        //    return result;
        //}
    }
}
