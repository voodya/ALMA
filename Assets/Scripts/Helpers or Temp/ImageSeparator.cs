using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

public static class ImageSeparator
{

    public static async Task<List<PuzzleElementData>> Separate(int size, Texture2D source, string name)
    {
        List<PuzzleElementData> puzzleElementDatas = new List<PuzzleElementData>();
        int _sourceWidht = source.width;
        int _sourceHeight = source.height;
        int _texturePartWidh;
        int _texturePartHeight;

        if (_sourceWidht % size != 0)
        {
            _sourceWidht -= _sourceWidht % size;
        }

        if (_sourceHeight % size != 0)
        { 
            _sourceHeight -= _sourceHeight % size;
        }
        _texturePartWidh = _sourceWidht / size;
        _texturePartHeight = _sourceHeight / size;

        int interations = size * size;
        int x = 0;
        int y = 0;

        string directory = $"{Application.persistentDataPath}/Resources/{name}/";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        for (int i = 0; i < interations; i++)
        {
            string FilePath = $"{directory}{size}_{i}.png";


            if (File.Exists(FilePath))
            {
                Texture2D temp = new Texture2D(1,1);
                temp.LoadImage(File.ReadAllBytes(FilePath));
                puzzleElementDatas.Add(new PuzzleElementData(new(x, y), FilePath));
            }
            else
            {
                Texture2D temp = new Texture2D(_texturePartWidh, _texturePartHeight);

                var a = source.GetPixels(x * _texturePartWidh, y * _texturePartHeight, _texturePartWidh, _texturePartHeight);
                Debug.Log(a.Length);
                temp.SetPixels(0, 0, _texturePartWidh, _texturePartHeight, a);


                //при использовании заранее подготовленных текстур можно использовать более оптимизированный алгоритм на GPU
                //Graphics.CopyTexture(source, 0, 0, x * _texturePartWidh, y * _texturePartHeight, _texturePartWidh, _texturePartHeight, temp, 0, 0, 0, 0);
                temp.Apply();
                File.WriteAllBytes(FilePath, temp.EncodeToPNG()); 
                puzzleElementDatas.Add(new PuzzleElementData( new(x, y), FilePath));
                
            }

            x++;
            if (x == size)
            {
                x = 0;
                y++;
            }
            await Task.Yield();
        }  
        return puzzleElementDatas;
    }
}
