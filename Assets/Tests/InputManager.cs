using QuestSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Objective objective;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ObjectiveManager.Instance.Broadcast(objective.GetHashCode());
        }
    }
}