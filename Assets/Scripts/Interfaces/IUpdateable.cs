using System;
using UnityEngine;

public interface IUpdateable<T>
{
    event Action<T> OnUpdated;
    
    void CustomUpdate(T obj);
    void RemoveAllListeners();
}
