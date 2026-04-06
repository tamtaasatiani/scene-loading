using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class QuestDisplayElement : MonoBehaviour
    {
        private Sprite _sprite;
        private Sprite _tick;
        private string _questName;
        private float _progress;
        private bool _progressible;
        
        [SerializeField] private Image icon;
        [SerializeField] private Image tick;
        [SerializeField] private TextMeshProUGUI questNameText;
        [SerializeField] private GameObject progressBar;

        public void Initialize(QuestUIElement? element)
        {
            _sprite = element?.Icon;
            _questName = element?.Name;
            if (element?.Progressible is true)
                _progress = (float)element?.ProgressValue;

            _progressible = element?.Progressible ?? false;

            if (element?.Tick != null)
                _tick = element?.Tick;
            
            icon.sprite = _sprite;
            tick.sprite = _tick;
            questNameText.text = _questName;

            progressBar.gameObject.SetActive(_progressible);
            
            progressBar.GetComponent<Slider>().value = _progress;
        }
    }
}
