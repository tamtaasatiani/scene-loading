using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneData scene;
    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        scene.Initialize();
    }

    public UniTask LoadScene()
    {
        loadingScreen.Initialize();
        
        if (scene.ScenePreview != null)
            loadingScreen.SetImage(scene.ScenePreview);
        
        loadingScreen.ShowLoadingScreen();
        
        var operation = SceneManager.LoadSceneAsync(scene.name);

        if (operation == null)
        {
            Debug.LogError($"Unable to load scene {scene.name}");
            return UniTask.CompletedTask;
        }
        
        while (!operation.isDone)
        {
            loadingScreen.SetSliderValue(operation.progress);
        }
        
        return UniTask.CompletedTask;
    }
}
