using QuestSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ObjectiveManager.Instance.Broadcast("PressBackspace");
        }
    }
}