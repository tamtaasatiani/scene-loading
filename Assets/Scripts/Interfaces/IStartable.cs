using System;
using UnityEngine;

public interface IStartable
{
    event Action<ScriptableObject> OnStarted;
    
    void CustomStart(ScriptableObject obj);
}
