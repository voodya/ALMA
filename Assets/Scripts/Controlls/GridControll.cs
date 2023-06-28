using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GridControll : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private RectTransform _container;
    [SerializeField] private AspectRatioFitter _fitter;
    [SerializeField] private List<GridCell> _cells;
    [SerializeField] private GridCell _cellPrefab;

    private LevelData _cashedLevelData;
    private int _correctCount = 0;

    public void Init(LevelData data)
    {
        _cashedLevelData = data;
        _correctCount = 0;
        GenerateGrid();
    }

    private async void CollectCorrectElements(bool isCorrect)
    {
        _correctCount += isCorrect ? 1 : -1;
        if (_correctCount == _cashedLevelData.Elements.Count)
        {
            await Task.Delay(1000);
            PanelsController.OnOpenPanel?.Invoke("Win", null);
        }
    }

    private void GenerateGrid()
    {
        _fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        _fitter.aspectRatio = (float)_cashedLevelData.TargetImage.width / _cashedLevelData.TargetImage.height;

        foreach (GridCell cell in _cells)
        {
            cell.gameObject.SetActive(false);
            cell.OnChangeCorrect -= CollectCorrectElements;
        }

        for (int i = 0; i < _cashedLevelData.Elements.Count; i++)
        {

            if (i + 1 > _cells.Count)
            {
                GridCell cell = Instantiate(_cellPrefab, _container);
                cell.Init(_cashedLevelData.Elements[i].MatrixPosition);
                cell.OnChangeCorrect += CollectCorrectElements;
                _cells.Add(cell);
            }
            else
            {
                _cells[i].gameObject.SetActive(true);
                _cells[i].OnChangeCorrect += CollectCorrectElements;
                _cells[i].Init(_cashedLevelData.Elements[i].MatrixPosition);
            }
        }
        _grid.cellSize = new Vector2(_container.rect.width / _cashedLevelData.Difficult, _container.rect.height / _cashedLevelData.Difficult);
    }
}
