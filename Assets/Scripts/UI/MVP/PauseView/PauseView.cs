using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP.Pause
{
    public class PauseView : View
    {
        [SerializeField] private Button close;
        

        public override void Initialize(IModel model)
        {
            close.onClick.AddListener(Dispose);
        }

        public void OnDestroy()
        {
            close.onClick.RemoveAllListeners();
        }
    }
}
