using DG.Tweening;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    private RectTransform _targetPanelRect;
    [HideInInspector] public bool _isOpen;

    public virtual void Init(LevelData data)
    {
        Debug.Log("No req data");
    }

    public void Show()
    {
        if (_targetPanelRect == null) _targetPanelRect = GetComponent<RectTransform>();
        _targetPanelRect.DOAnchorPos(Vector2.zero, 0.5f);
        _isOpen = true;
    }

    public void Hide()
    {
        _isOpen = false;

        if (_targetPanelRect == null) _targetPanelRect = GetComponent<RectTransform>();

        _targetPanelRect.DOAnchorPos(new(0, Screen.height * 2), 0.5f);
    }
}
