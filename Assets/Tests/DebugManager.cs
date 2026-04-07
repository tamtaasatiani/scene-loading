using System;
using Cysharp.Threading.Tasks;
using QuestSystem;
using ServiceLocation;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private Objective obj;
    
    private void OnEnable()
    {
        IServiceLocator.Default.GetService<ObjectiveManager>().AddListenerAsync(obj.GetHashCode(), NotifyBackspacePressed).Forget();  
    }

    private void NotifyBackspacePressed(ScriptableObject objective)
    {
        Debug.Log("Backspace pressed");
    }
    
    private void OnDisable()
    {
        IServiceLocator.Default.GetService<ObjectiveManager>().RemoveListenerAsync(obj.GetHashCode(), NotifyBackspacePressed).Forget();
    }
}
