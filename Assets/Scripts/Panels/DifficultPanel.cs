using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultPanel : PanelBase
{
    [SerializeField] private Button _return;
    [SerializeField] private List<DifficultBtns> _btns;

    private LevelData _cashedData;

    private void Start()
    {
        foreach (DifficultBtns btn in _btns)
        {
            btn.Btn.onClick.RemoveAllListeners();
            btn.Btn.onClick.AddListener(() => GeneratePuzzleElements(btn.Difficult));
        }
        _return.onClick.RemoveAllListeners();
        _return.onClick.AddListener(Return);
    }


    public override void Init(LevelData data)
    {
        _cashedData = data;
    }

    private void Return()
    {
        PanelsController.OnOpenPanel?.Invoke("Main", _cashedData);
    }

    private async void GeneratePuzzleElements(int difficult)
    {
        _cashedData.Difficult = difficult;
        _cashedData.Elements = await ImageSeparator.Separate(_cashedData.Difficult, _cashedData.TargetImage, _cashedData.Name);
        PanelsController.OnOpenPanel?.Invoke("Game", _cashedData);
    }
}

[System.Serializable]
public struct DifficultBtns
{
    public Button Btn;
    public int Difficult;
}

