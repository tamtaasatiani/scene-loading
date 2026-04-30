using System;
using UnityEngine;

namespace UI.MVP.Pause
{
    [CreateAssetMenu(menuName = "UI/MVP/Pause/Model")]
    public class PauseModel : Model
    {
        public QuestUIManager QuestUIManager;
        public Action OnClose;
        public SceneData mainMenuScene;
    }
}
