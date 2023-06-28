using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelBase
{
    [SerializeField] private GridControll _grid;
    [SerializeField] private PuzzleController _puzzle;
    [SerializeField] private Button _return;


    private void Start()
    {
        _return.onClick.AddListener(Return);
    }

    private void Return()
    {
        PanelsController.OnOpenPanel?.Invoke("Main", null);
    }

    public override void Init(LevelData data)
    {
        _grid.Init(data);
        _puzzle.Init(data);
    }
}
