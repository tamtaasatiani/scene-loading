using UI.MVP.Pause;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    private bool _paused;
    
    [SerializeField] private Canvas canvas;

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
        if (Input.GetKey(KeyCode.Escape))
        {
            TransitionToPauseState();
        }
    }

    private void UpdatePauseState()
    {
        
    }

    private void TransitionToPauseState()
    {
        Cursor.lockState = CursorLockMode.None;
        _paused = true;

        var pauseModel = new PauseModel();
        pauseModel.OnClose += TransitionToPlayState;
        var presenter = new PausePresenter(pauseModel, canvas);
    }
    
    private void TransitionToPlayState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _paused = false;
    }
}
