using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image image;
    
    [SerializeField] private Slider slider;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

    
}
