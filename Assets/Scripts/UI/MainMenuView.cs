using System;
using Cysharp.Threading.Tasks;
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
    
    
    public event UnityAction loadLightScene;
    public event UnityAction loadHeavyScene;

    private void Awake()
    {
        loadLightScene = LoadLightScene;
        loadHeavyScene = LoadHeavyScene;
        
        lightSceneButton.onClick.AddListener(loadLightScene);
        heavySceneButton.onClick.AddListener(loadHeavyScene);
    }

    private void LoadLightScene()
    {
        sceneLoader.LoadScene(lightSceneData);
    }

    private void LoadHeavyScene()
    {
        sceneLoader.LoadScene(heavySceneData);
    }
}
