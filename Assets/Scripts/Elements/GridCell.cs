using System;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public Vector2 _coord;
    public bool _isEmpty = true;
    public bool _isCorrect = false;
    public Transform _transform;
    [SerializeField] private Image _raycastTargetImage;
    public Action<bool> OnChangeCorrect;
    public PuzzleElement _targetElem;

    public void Init(Vector2 position)
    {
        _isEmpty = true;
        _isCorrect = false;
        _coord = position;
        _raycastTargetImage.raycastTarget = true;
    }

    public void SetPuzzle(Vector2 pos, PuzzleElement puz)
    {
        Debug.Log($"{gameObject.name} SetPuzzle");
        _targetElem = puz;
        _isEmpty = false;
        _isCorrect = pos == _coord;
        _raycastTargetImage.raycastTarget = false;
        if (_isCorrect) OnChangeCorrect?.Invoke(true);
        //if (_isCorrect) _raycastTargetImage.color = Color.green;
        //else _raycastTargetImage.color = Color.red;
    }

    public void RemovePuzzle()
    {
        Debug.Log($"{gameObject.name} RemovePuzzle");
        if (_isCorrect) OnChangeCorrect?.Invoke(false);
        _targetElem = null;
        _isCorrect = false;
        _isEmpty = true;
        _raycastTargetImage.raycastTarget = true;

        //if (_isCorrect) _raycastTargetImage.color = Color.green;
        //else _raycastTargetImage.color = Color.red;
    }
}
