using System;
using UnityEngine;

public interface ICompletable
{
    event Action<ScriptableObject> OnCompleted;
    
    void Complete(ScriptableObject obj);
}
