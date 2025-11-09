using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite scenePreview;
    
    private Scene scene;

    public void Initialize()
    {
        scene = SceneManager.GetSceneByName(name);

        if (scene == null)
        {
            throw new System.Exception("Scene not found: " + name);
        }
    }

    public async UniTask LoadScene()
    {
        await SceneManager.LoadSceneAsync(name);
    }
}
