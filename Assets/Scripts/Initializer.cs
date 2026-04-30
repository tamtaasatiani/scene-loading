using System;
using Cysharp.Threading.Tasks;
using ServiceLocation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    [SerializeField] private SceneData managersScene;
    [SerializeField] private SceneData mainMenuScene;

    private SceneInstance _firstScene;
    
    public event Action<float> OnLoadingStarted;
    public event Action<float> OnLoadingUpdated;
    public event Action<float> OnLoadingFinished;


    private void Awake()
    {
        InitializeAsync().Forget();
    }

    private async UniTask InitializeAsync()
    {
        var initializationScene = SceneManager.GetActiveScene();
        await LoadSceneAsync(managersScene, LoadSceneMode.Additive);
        var serviceInstaller = FindAnyObjectByType<ServiceInstaller>();
        await serviceInstaller.InitializeAsync();
        await LoadSceneAsync(mainMenuScene, LoadSceneMode.Additive);
        await SceneManager.UnloadSceneAsync(initializationScene);
        IServiceLocator.Default.GetService<SceneLoader>().SetFirstScene(_firstScene);
        var mScene = SceneManager.GetSceneByName(managersScene.name);
        await SceneManager.UnloadSceneAsync(mScene);
    }
    
    private async UniTask LoadSceneAsync(SceneData scene, LoadSceneMode mode)
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

            _firstScene = operation.Result;
            OnLoadingStarted?.Invoke(1);
        }
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
}
