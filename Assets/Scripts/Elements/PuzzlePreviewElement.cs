using UnityEngine;
using UnityEngine.UI;

public class PuzzlePreviewElement : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private RectTransform _rectAspect;
    [SerializeField] private RectTransform _parentSizeDelta;

    private Vector2 _cellSize;
    private Texture2D _preview;

    public void Init(Texture2D prev, Vector2 cellSize)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(ChoicePuzzle);
        _cellSize = cellSize;
        _preview = prev;
        _targetImage.texture = _preview;
        SetAspectRatio();
    }

    private void ChoicePuzzle()
    {
        PanelsController.OnOpenPanel?.Invoke("Difficult", new LevelData(_preview, _preview.name));
    }

    private void SetAspectRatio()
    {
        float coef;

        if (_preview.width > _preview.height)
        {
            coef = (float)_preview.width / _cellSize.x; 
        }
        else
        {
            coef = (float)_preview.height / _cellSize.y;
        }

        _rectAspect.sizeDelta = new Vector2(_preview.width / coef, _preview.height / coef);
    }
}
