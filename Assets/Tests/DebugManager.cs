using System;
using QuestSystem;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private Objective obj;
    
    private void OnEnable()
    {
        //ObjectiveManager.Instance.AddListener(obj.GetHashCode(), NotifyBackspacePressed);  
    }

    private void NotifyBackspacePressed(ScriptableObject objective)
    {
        Debug.Log("Backspace pressed");
    }
    
    private void OnDisable()
    {
        //ObjectiveManager.Instance.RemoveListener(obj.GetHashCode(), NotifyBackspacePressed);
    }
}
