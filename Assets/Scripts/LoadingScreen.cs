using BetterAttributes.Runtime;
using Cysharp.Threading.Tasks;
using ServiceLocation;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : Service
{
    private SceneLoader _sceneLoader;
    
    [SerializeField, Require] private Image image;
    [SerializeField, Require] private Slider slider;

    public override UniTask InitializeAsync()
    {
        _sceneLoader = IServiceLocator.Default.GetService<SceneLoader>();
        
        _sceneLoader.OnLoadingStarted += StartLoading;
        _sceneLoader.OnLoadingUpdated += SetSliderValue;
        _sceneLoader.OnLoadingFinished += FinishLoading;
        
        return base.InitializeAsync();
    }

    private void OnDestroy()
    {
        _sceneLoader.OnLoadingStarted -= StartLoading;
        _sceneLoader.OnLoadingUpdated -= SetSliderValue;
        _sceneLoader.OnLoadingFinished -= FinishLoading;
    }

    public void SetImage(Sprite sprite)
    {
        if (image == null || image.sprite == null) return;
        image.sprite = sprite;
    }

    private void StartLoading(float value)
    {
        this.gameObject.SetActive(true);
        SetSliderValue(0);
    }

    private void FinishLoading(float value)
    {
        this.gameObject.SetActive(false);
        SetSliderValue(1);
    }

    public void SetSliderValue(float value)
    {
        if (slider == null)
        {
            Debug.LogWarning("No slider is set");
            return;
        }
        slider.value = value;
    }
}
