using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image image;
    
    [SerializeField] private Slider slider;

    public void Initialize()
    {
        image = GetComponentInChildren<Image>();
        slider = GetComponentInChildren<Slider>();
    }

    public void SetImage(Sprite sprite)
    {
        if (image == null || image.sprite == null) return;
        image.sprite = sprite;
    }

    public void ShowLoadingScreen()
    {
        this.gameObject.SetActive(true);
    }

    public void SetSliderValue(float value)
    {
        if (slider == null) return;
        slider.value = value;
    }

    
}
