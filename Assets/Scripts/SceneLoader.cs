using System;
using Cysharp.Threading.Tasks;
using ServiceLocation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : Service
{
    [SerializeField] private LoadingScreen loadingScreen;

    private bool _currentSceneSet = false;
    private SceneInstance _currentScene;

    public override UniTask InitializeAsync()
    {
        base.InitializeAsync();
        _initialized = true;
        return UniTask.CompletedTask;
    }

    public void SetFirstScene(SceneInstance scene)
    {
        if (_currentSceneSet)
        {
            Debug.LogError("Scene is already set");
            return;
        }
        
        _currentScene = scene;
        _currentSceneSet = true;
    }

    public async UniTask LoadSceneAsync(SceneData scene, LoadSceneMode mode)
    {
        try
        {
            if (scene == null)
            {
                Debug.LogError($"Scene is empty or unavailable");
            }
            
            if(!scene.IsValid())
                return;
            loadingScreen.Initialize();

            if (scene.ScenePreview != null)
                loadingScreen.SetImage(scene.ScenePreview);

            loadingScreen.ShowLoadingScreen();
            await UniTask.DelayFrame(1);

            var operation = Addressables.LoadSceneAsync(scene.name, mode);

            if (operation.Equals(null))
            {
                Debug.LogError($"Unable to load scene {scene.name}");
                return;
            }
            do
            {
                await UniTask.DelayFrame(1);
                loadingScreen.SetSliderValue(operation.GetDownloadStatus().Percent);
            } while (!operation.GetDownloadStatus().IsDone);
            
            if (_currentSceneSet)
                await Addressables.UnloadSceneAsync(_currentScene);
            
            _currentScene = operation.Result;
            _currentSceneSet = true;
            loadingScreen.HideLoadingScreen();
        }
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
    
    public async UniTask LoadSceneAsync(SceneData scene)
    {
        try
        {
            if (scene == null)
            {
                Debug.LogError($"Scene is empty or unavailable");
            }
            
            if(!scene.IsValid())
                return;
            loadingScreen.Initialize();

            if (scene.ScenePreview != null)
                loadingScreen.SetImage(scene.ScenePreview);

            loadingScreen.ShowLoadingScreen();
            await UniTask.DelayFrame(1);

            var operation = Addressables.LoadSceneAsync(scene.name);

            if (operation.Equals(null))
            {
                Debug.LogError($"Unable to load scene {scene.name}");
                return;
            }

            while (!operation.GetDownloadStatus().IsDone)
            {
                loadingScreen.SetSliderValue(operation.GetDownloadStatus().Percent);
                await UniTask.DelayFrame(1);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
}
