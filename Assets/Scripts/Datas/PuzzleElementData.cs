using UnityEngine;

[System.Serializable]
public class PuzzleElementData
{
    public Vector2 MatrixPosition;
    public string TexturePath;

    public PuzzleElementData(Vector2 pose, string path)
    {
        MatrixPosition = pose;
        TexturePath = path;
    }
}
