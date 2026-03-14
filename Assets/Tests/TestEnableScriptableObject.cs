using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Objective/Test")]
public class TestEnableScriptableObject : ScriptableObject
{
    private void OnEnable()
    {
        Debug.Log("Scriptable object enabled");
    }

    private void OnDisable()
    {
        Debug.Log("Scriptable object disabled");
    }
}
