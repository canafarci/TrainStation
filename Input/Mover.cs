using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool IsActive = true;
    [SerializeField] protected float _speed, _rotateSpeed;
    public Vector3 RotationOffset;
    bool _disabledMovement = false;
    InputReader _reader;
    public void DisableMovement() => _disabledMovement = true;
    public void EnableMovement() => _disabledMovement = false;
    private void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
    }

    private void Update()
    {
        if (!IsActive) return;
        Move(_reader.ReadInput());
    }

    void Move(Vector2 input)
    {
        if (input == Vector2.zero) return;

        Vector3 moveVector = RotateTowardsUp(new Vector3(input.x, 0, input.y));

        transform.rotation = Quaternion.LookRotation(moveVector);
        if (!_disabledMovement)
            transform.position += (moveVector.normalized * Time.deltaTime * _speed);
    }

    Vector3 RotateTowardsUp(Vector3 start)
    {
        // if you know start will always be normalized, can skip this step
        start.Normalize();

        Vector3 axis = Vector3.Cross(start, Vector3.right);

        return Quaternion.AngleAxis(RotationOffset.y, Vector3.up) * start;
    }
}