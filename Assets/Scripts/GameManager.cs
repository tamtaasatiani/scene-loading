using UI.MVP.Pause;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool _paused;
    
    [SerializeField] private Canvas canvas;
    [SerializeField] private PauseModel pauseModel;
    [SerializeField] private PausePresenter pausePresenter;

    private void Update()
    {
        if (!_paused)
        {
            UpdatePlayState();
            return;
        }

        UpdatePauseState();
    }

    private void UpdatePlayState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TransitionToPauseState();
        }
    }

    private void UpdatePauseState()
    {
        
    }

    private void TransitionToPauseState()
    {
        if (pausePresenter == null)
        {
            Debug.LogError("PausePresenter not provided");
            return;
        }

        if (pauseModel == null)
        {
            Debug.LogWarning("PauseModel not provided, instantiating manually...");
            pauseModel = ScriptableObject.CreateInstance<PauseModel>();
        }
        
        Cursor.lockState = CursorLockMode.None;
        _paused = true;

        pauseModel.OnClose += TransitionToPlayState;
        pausePresenter.Initialize(pauseModel, canvas);
    }
    
    private void TransitionToPlayState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _paused = false;
    }
}
