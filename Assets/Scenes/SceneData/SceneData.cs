using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite scenePreview;
    
    public Sprite ScenePreview => scenePreview;
    
    private Scene scene;

    public void Initialize()
    {
        scene = SceneManager.GetSceneByName(name);

        if (scene == null)
        {
            Debug.LogError($"Scene {name} not found!");
            return;
        }
    }
}
