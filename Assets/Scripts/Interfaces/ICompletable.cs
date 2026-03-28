using System;
using UnityEngine;

public interface ICompletable<T>
{
    event Action<T> OnCompleted;
    
    void Complete(T obj);
}
