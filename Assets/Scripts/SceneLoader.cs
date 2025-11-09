using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;

    public async UniTask LoadScene(SceneData scene)
    {
        if (scene == null)
        {
            Debug.LogError($"Scene is empty or unavailable");
        }
        
        loadingScreen.Initialize();
        
        if (scene.ScenePreview != null)
            loadingScreen.SetImage(scene.ScenePreview);
        
        loadingScreen.ShowLoadingScreen();
        
        var operation = SceneManager.LoadSceneAsync(scene.name);

        if (operation == null)
        {
            Debug.LogError($"Unable to load scene {scene.name}");
            return;
        }
        
        while (!operation.isDone)
        {
            loadingScreen.SetSliderValue(operation.progress);
            await UniTask.DelayFrame(1);
        }
    }
}
