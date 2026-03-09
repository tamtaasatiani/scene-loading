using System;
using QuestSystem;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private void OnEnable()
    {
        ObjectiveManager.Instance.AddListener("PressBackspace", NotifyBackspacePressed);  
    }

    private void NotifyBackspacePressed(Objective objective)
    {
        Debug.Log("Backspace pressed");
    }
    
    private void OnDisable()
    {
        ObjectiveManager.Instance.RemoveListener("PressBackspace", NotifyBackspacePressed);
    }
}
