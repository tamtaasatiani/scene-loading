using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy (Instance);
        }

        Instance = this as T;
    }
}
