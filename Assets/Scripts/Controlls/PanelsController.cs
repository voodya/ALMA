using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    [SerializeField] private List<PanelObj> _panels;
    public static Action<string, LevelData> OnOpenPanel;

    private void Start()
    {
        OnOpenPanel += OpenPanel;
    }

    private void OpenPanel(string obj, LevelData data)
    {
        foreach (var panel in _panels)
        {
            if (panel.Key == obj)
            {
                panel.Panel.Init(data);
                panel.Panel.Show();
            }
            else
                panel.Panel.Hide();
        }
    }
}

[System.Serializable]
public struct PanelObj
{
    public PanelBase Panel;
    public string Key;
}

