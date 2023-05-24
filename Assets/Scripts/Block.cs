using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum MainAxis
{
    X,
    Y,
    Z
}

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _rotationTime = .2f;

    private bool _moving = false;
    private MainAxis _mainAxis = MainAxis.Y;
    private Transform _pivotTransform;
    [SerializeField] private Transform _cubeVisual;
    private Direction _storedDirection;

    private void Awake()
    {
        _pivotTransform = new GameObject("PivotHelper").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate(Direction.Up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) Rotate(Direction.Down);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Rotate(Direction.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Rotate(Direction.Right);

        if (_moving)
            return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            _mainAxis = MainAxis.Y;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

    }

    private void Rotate(Direction direction)
    {
        if (_moving)
        {
            _storedDirection = direction;
            return;
        }

        _storedDirection = Direction.None;
        Vector3 pivotPoint;
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Up:
                pivotPoint = _cubeVisual.position + Vector3.forward * (_mainAxis == MainAxis.Z ? 1 : .5f);
                pivotPoint.y = 0;
                StartCoroutine(RotateCoroutine(pivotPoint, Vector3.right));
                _mainAxis = _mainAxis == MainAxis.Y ? MainAxis.Z : _mainAxis == MainAxis.Z ? MainAxis.Y : _mainAxis;
                break;
            case Direction.Down:
                pivotPoint = _cubeVisual.position + Vector3.back * (_mainAxis == MainAxis.Z ? 1 : .5f);
                pivotPoint.y = 0;
                StartCoroutine(RotateCoroutine(pivotPoint, Vector3.left));
                _mainAxis = _mainAxis == MainAxis.Y ? MainAxis.Z : _mainAxis == MainAxis.Z ? MainAxis.Y : _mainAxis;
                break;
            case Direction.Left:
                pivotPoint = _cubeVisual.position + Vector3.left * (_mainAxis == MainAxis.X ? 1 : .5f);
                pivotPoint.y = 0;
                StartCoroutine(RotateCoroutine(pivotPoint, Vector3.forward));
                _mainAxis = _mainAxis == MainAxis.Y ? MainAxis.X : _mainAxis == MainAxis.X ? MainAxis.Y : _mainAxis;
                break;
            case Direction.Right:
                pivotPoint = _cubeVisual.position + Vector3.right * (_mainAxis == MainAxis.X ? 1 : .5f);
                pivotPoint.y = 0;
                StartCoroutine(RotateCoroutine(pivotPoint, Vector3.back));
                _mainAxis = _mainAxis == MainAxis.Y ? MainAxis.X : _mainAxis == MainAxis.X ? MainAxis.Y : _mainAxis;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private IEnumerator RotateCoroutine(Vector3 pivotPoint, Vector3 rotationAxis)
    {
        _moving = true;
        _rigidbody.isKinematic = true;
        var t = 0f;
        _pivotTransform.position = pivotPoint;
        _pivotTransform.rotation = Quaternion.identity;
        transform.parent = _pivotTransform;
        while (t < 1)
        {
            t += Time.deltaTime / _rotationTime;
            _pivotTransform.localEulerAngles = rotationAxis * 90 * Easing.InSine(t);
            yield return null;
        }
        _pivotTransform.localEulerAngles = rotationAxis * 90;
        transform.parent = null;
        var pos = transform.position;
        pos.x = Mathf.RoundToInt(pos.x * 2) * .5f;
        pos.z = Mathf.RoundToInt(pos.z * 2) * .5f;
        _rigidbody.isKinematic = false;
        _moving = false;
        if(_storedDirection != Direction.None)
            Rotate(_storedDirection);
    }
}
