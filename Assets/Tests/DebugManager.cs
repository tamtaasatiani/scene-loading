using System;
using Cysharp.Threading.Tasks;
using QuestSystem;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private Objective obj;
    
    private void OnEnable()
    {
        ObjectiveManager.Instance.AddListenerAsync(obj.GetHashCode(), NotifyBackspacePressed).Forget();  
    }

    private void NotifyBackspacePressed(ScriptableObject objective)
    {
        Debug.Log("Backspace pressed");
    }
    
    private void OnDisable()
    {
        ObjectiveManager.Instance.RemoveListenerAsync(obj.GetHashCode(), NotifyBackspacePressed).Forget();
    }
}
