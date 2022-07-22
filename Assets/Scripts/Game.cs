using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas _endScreen;
    [SerializeField] private Handle _handle;
    [SerializeField] private CanvasGroup _endScreenCanvasGroup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private Animator _animator;       

    private void OnEnable()
    {
        _handle.IsGrounded += OnHandleGrounded;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _handle.IsGrounded -= OnHandleGrounded;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnHandleGrounded()
    {
        _cameraFollower.Zoom();        

        Invoke(nameof(OpenEndScreen), 1f);        
    }

    private void OnRestartButtonClick()
    {
        CloseEndScreen();

        SecondScene.Load();        
    }

    private void OpenEndScreen()
    {        
        _endScreenCanvasGroup.alpha = 1;

        _restartButton.interactable = true;

        _animator.SetBool("IsEnd", true);
    }

    private void CloseEndScreen()
    {
        Time.timeScale = 1;

        _endScreenCanvasGroup.alpha = 0;

        _restartButton.interactable = false;
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }
}