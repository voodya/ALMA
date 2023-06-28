using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScrollPanel : PanelBase
{
    [SerializeField] private List<Texture2D> _sourceImages;
    [SerializeField] private ScrollRect _objectContainer;
    [SerializeField] private PuzzlePreviewElement _prefab;
    [SerializeField] private List<PuzzlePreviewElement> _preloadedElements;
    [SerializeField] private GridLayoutGroup _layoutGroup;

    public void Start()
    {
        GeneatePuzzlesPreview();
    }

    private void GeneatePuzzlesPreview()
    {
        foreach (var element in _preloadedElements)
        {
            element.gameObject.SetActive(false);
        }
        for (int i = 0; i < _sourceImages.Count; i++)
        {
            if (i + 1 > _preloadedElements.Count)
            {
                PuzzlePreviewElement newElem = Instantiate(_prefab, _objectContainer.content);
                _preloadedElements.Add(newElem);
                newElem.Init(_sourceImages[i], _layoutGroup.cellSize);
            }
            else
            {
                _preloadedElements[i].Init(_sourceImages[i], _layoutGroup.cellSize);
                _preloadedElements[i].gameObject.SetActive(true);
            }
        }
    }
}
