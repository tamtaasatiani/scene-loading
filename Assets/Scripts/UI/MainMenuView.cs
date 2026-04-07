using System;
using Cysharp.Threading.Tasks;
using ServiceLocation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button lightSceneButton;
    [SerializeField] private Button heavySceneButton;
    
    [SerializeField] private SceneData lightSceneData;
    [SerializeField] private SceneData heavySceneData;

    //[FormerlySerializedAs("sceneLoader")] [SerializeField] private SceneLoaderObsolete sceneLoaderObsolete;
    
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
        IServiceLocator.Default.GetService<SceneLoader>().LoadSceneAsync(lightSceneData, LoadSceneMode.Additive).Forget();
        //sceneLoaderObsolete.LoadScene(lightSceneData).Forget();
    }

    private void HandleLoadHeavyScene()
    {
        IServiceLocator.Default.GetService<SceneLoader>().LoadSceneAsync(heavySceneData, LoadSceneMode.Additive).Forget();
        //sceneLoaderObsolete.LoadScene(heavySceneData).Forget();
    }
}
