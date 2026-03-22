using System;
using UnityEngine;

public interface IUpdateable
{
    event Action<ScriptableObject> OnUpdated;
    
    void CustomUpdate(ScriptableObject obj);
    void RemoveAllListeners();
}
