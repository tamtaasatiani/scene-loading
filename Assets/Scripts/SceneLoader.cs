using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;

    public async UniTask LoadScene(SceneData scene)
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
        catch (Exception exception)
        {
            Debug.LogError($"An error occured while loading scene: {exception}");
        }
    }
}
