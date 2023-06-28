using UnityEngine;
using UnityEngine.UI;

public class WinPanel : PanelBase
{
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _retryBtn;

    private void Start()
    {
        _exitBtn.onClick.AddListener(Exit);
        _retryBtn.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        PanelsController.OnOpenPanel?.Invoke("Main", null);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
