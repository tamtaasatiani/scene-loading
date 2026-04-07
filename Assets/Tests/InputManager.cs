using Cysharp.Threading.Tasks;
using QuestSystem;
using ServiceLocation;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Objective objective;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            IServiceLocator.Default.GetService<ObjectiveManager>().BroadcastAsync(objective.GetHashCode()).Forget();
        }
    }
}