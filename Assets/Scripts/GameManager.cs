using UI.MVP.Pause;
using UnityEngine;

public class GameManager : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Escape))
        {
            TransitionToPlayState();
        }
    }

    private void TransitionToPauseState()
    {
        Cursor.lockState = CursorLockMode.None;
        _paused = true;
        var presenter = new PausePresenter();
        
    }
    
    private void TransitionToPlayState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _paused = false;
    }
}
