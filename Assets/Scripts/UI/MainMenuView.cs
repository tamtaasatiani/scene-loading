using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button lightSceneButton;
    [SerializeField] private Button heavySceneButton;
    
    [SerializeField] private SceneData lightSceneData;
    [SerializeField] private SceneData heavySceneData;

    [SerializeField] private SceneLoader sceneLoader;
    
    
    public event UnityAction LoadLightScene;
    public event UnityAction LoadHeavyScene;

    private void Awake()
    {
        LoadLightScene = HandleLoadLightScene;
        LoadHeavyScene = HandleLoadHeavyScene;
        
        lightSceneButton.onClick.AddListener(LoadLightScene);
        heavySceneButton.onClick.AddListener(LoadHeavyScene);
    }

    private void HandleLoadLightScene()
    {
        sceneLoader.LoadScene(lightSceneData);
    }

    private void HandleLoadHeavyScene()
    {
        sceneLoader.LoadScene(heavySceneData);
    }
}
