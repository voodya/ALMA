using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class PuzzleElement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private Transform _transform;

    private PuzzleElementData _cashedData;
    private GridCell _cashedCell;
    private Vector2 _startPosition;

    public async void Init(PuzzleElementData data)
    {
        _cashedData = data;
        Texture2D Temp = new Texture2D(1,1);
        Temp.LoadImage(await File.ReadAllBytesAsync(_cashedData.TexturePath));
         
        _targetImage.texture = Temp;
        _startPosition = Vector2.zero;
    }


    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _targetImage.color = new(1f, 1f, 1f, 0.5f);
        if (_startPosition == Vector2.zero) _startPosition = _transform.position;
        if(_cashedCell != null)
        if (_cashedCell._targetElem == this)
        {
            _cashedCell.RemovePuzzle();
            _cashedCell = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _targetImage.color = new(1f, 1f, 1f, 1f);
        List<GameObject> objects = eventData.hovered;
        foreach (var obj in objects)
        {
            if (obj.tag == "Cell")
            {
                _transform.DOMove(obj.transform.position, 0.1f);
                _cashedCell = obj.GetComponent<GridCell>();
                _cashedCell.SetPuzzle(_cashedData.MatrixPosition, this);
                return;
            }
        }
        _transform.DOMove(_startPosition, 0.1f);
    }
}
