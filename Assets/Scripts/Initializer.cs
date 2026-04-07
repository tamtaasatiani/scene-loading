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
    [SerializeField] private LoadingScreen loadingScreen;

    private SceneInstance _firstScene;

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
        SceneManager.UnloadSceneAsync(initializationScene);
        IServiceLocator.Default.GetService<SceneLoader>().SetFirstScene(_firstScene);
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

            _firstScene = operation.Result;
        }
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
}
