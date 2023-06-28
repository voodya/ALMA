using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PuzzleElement _prefab;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private List<PuzzleElement> _preloadedElems;
    [SerializeField] private AspectRatioFitter _fitter;

    private LevelData _cashedLevelData;

    public void Init(LevelData data)
    {
        _cashedLevelData = data;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _grid.enabled = true;
        _fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        _fitter.aspectRatio = (float)_cashedLevelData.TargetImage.width / _cashedLevelData.TargetImage.height;


        foreach (var cell in _preloadedElems)
        {
            cell.gameObject.SetActive(false);
        }
        List<PuzzleElementData> shuffled = _cashedLevelData.Elements;

        shuffled = Shuffler.Shuffle(shuffled);
        for (int i = 0; i < shuffled.Count; i++)
        {
            if (i + 1 > _preloadedElems.Count)
            {
                PuzzleElement cell = Instantiate(_prefab, _parent);
                cell.Init(shuffled[i]);

                _preloadedElems.Add(cell);
            }
            else
            {
                _preloadedElems[i].gameObject.SetActive(true);
                _preloadedElems[i].Init(shuffled[i]);
            }
        }


        _grid.cellSize = new Vector2(_parent.rect.width / _cashedLevelData.Difficult, _parent.rect.height / _cashedLevelData.Difficult);
        //_grid.enabled = false;
    }
}
