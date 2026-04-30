using System;
using Cysharp.Threading.Tasks;
using ServiceLocation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : Service
{
    private bool _currentSceneSet = false;
    private SceneInstance _currentScene;

    public event Action<float> OnLoadingStarted;
    public event Action<float> OnLoadingUpdated;
    public event Action<float> OnLoadingFinished;

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

            OnLoadingStarted?.Invoke(0);
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
                OnLoadingUpdated?.Invoke(operation.GetDownloadStatus().Percent);
            } while (!operation.GetDownloadStatus().IsDone);
            
            if (_currentSceneSet)
                await Addressables.UnloadSceneAsync(_currentScene);
            
            _currentScene = operation.Result;
            _currentSceneSet = true;
            OnLoadingFinished?.Invoke(1);
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

            OnLoadingStarted?.Invoke(0);
            await UniTask.DelayFrame(1);

            var operation = Addressables.LoadSceneAsync(scene.name);

            if (operation.Equals(null))
            {
                Debug.LogError($"Unable to load scene {scene.name}");
                return;
            }

            while (!operation.GetDownloadStatus().IsDone)
            {
                OnLoadingUpdated?.Invoke(operation.GetDownloadStatus().Percent);
                await UniTask.DelayFrame(1);
            }
            
            OnLoadingFinished?.Invoke(1);
        }
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
}
