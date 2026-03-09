using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            
            _instance = FindFirstObjectByType<T>();
            
            if (_instance != null)
                return _instance;

            SetupInstance();
            return _instance;
        }
    }
    
    public virtual void Awake()
    {
        RemoveDuplicates();
    }
    
    private static void SetupInstance()
    {
        _instance = FindFirstObjectByType<T>();
        
        if (_instance != null)
            return;
        
        GameObject gameObj = new GameObject();
        gameObj.name = typeof(T).Name;
        _instance = gameObj.AddComponent<T>();
        DontDestroyOnLoad(gameObj);
    }
    
    private void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
