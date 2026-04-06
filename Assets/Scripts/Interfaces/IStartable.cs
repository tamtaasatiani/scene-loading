using System;
using UnityEngine;

public interface IStartable<T>
{
    event Action<T> OnStarted;
    
    void CustomStart(T obj);
}
