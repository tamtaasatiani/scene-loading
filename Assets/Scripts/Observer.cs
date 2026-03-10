using System;
using QuestSystem;
using UnityEngine;

public class Observer<TManager, TObserved> : SingletonMonoBehaviour<TManager> where TManager : MonoBehaviour where TObserved : ScriptableObject, IUpdateable
{
    [SerializeField] protected Library<TObserved> library;

    public void AddListener(int hashCode, Action callback)
    {
        if (library == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in empty library");
            return;
        }
        
        var item = library.FindByHash(hashCode);
        
        if (item == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in library");
            return;
        }
        
        item.OnUpdated += callback;
    }

    public void RemoveListener(int hashCode, Action callback)
    {
        if (library == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in empty library");
            return;
        }
        
        var item = library.FindByHash(hashCode);
        
        if (item == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in library");
            return;
        }
        
        item.OnUpdated -= callback;
    }

    public void RemoveAllListeners()
    {
        foreach (var item in library.GetAll())
        {
            item.RemoveAllListeners();
        }
    }
}
