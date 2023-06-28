using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public Texture2D TargetImage;
    public string Name;
    public int Difficult;
    public List<PuzzleElementData> Elements;

    public LevelData(Texture2D img, string name)
    {
        TargetImage = img;
        Name = name;
    }
}
