using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchSystem : MonoBehaviour
{
    private float _moveTime;
    private Tweener _tweener;
    private int _index;
    private bool _isStarted;
    private List<Vector2> _positions = new List<Vector2>();
    private Vector2 _position;

    public bool IsMoving { get; private set; }

    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private float _maxY = 3.77f;

    private void Update()
    {
        AndroidInput();
#if UNITY_EDITOR
        PC_TestInput();
#endif
    }

    private void PC_TestInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            
            AddPos(_position);
        }
    }

    private void AndroidInput()
    {
        if (Input.touches.Length == 1 && Input.touches[0].phase == TouchPhase.Began )
        {
            _position = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y));

            AddPos(_position);
        }
    }

    private void AddPos(Vector2 position)
    {
        if (position.y <= _maxY)
        {
            _positions.Add(position);

            if (!_isStarted)
            {
                StartCoroutine(MoveObject());
                _isStarted = true;
            }
        }
    }

    private IEnumerator MoveObject()
    {
        while (_index <= _positions.Count - 1)
        {
            _tweener = obj.transform.DOMove(_positions[_index], _moveTime);
                
            yield return _tweener.WaitForCompletion();
            
            _index++;
        }
        _isStarted = false;
    }

    public void ChangeMoveTime(float speed)
    {
        _moveTime = 1/speed;
    }
}
