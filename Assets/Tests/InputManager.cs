using Cysharp.Threading.Tasks;
using QuestSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Objective objective;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ObjectiveManager.Instance.BroadcastAsync(objective.GetHashCode()).Forget();
        }
    }
}