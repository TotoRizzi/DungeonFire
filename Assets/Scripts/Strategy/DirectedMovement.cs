using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : IMovement
{
    float _speed;
    Transform _transform;
    Transform _target;
    Rigidbody _rb;

    Vector3 directionToTarget;

    public DirectedMovement(float speed, Transform transform, Transform target, Rigidbody rb)
    {
        _speed = speed;
        _transform = transform;
        _target = target;
        _rb = rb;
    }
    public void Move()
    {
        directionToTarget = (_target.transform.position - _transform.position).normalized;

       // _transform.position += directionToTarget * _speed * Time.deltaTime;
        _rb.MovePosition(_transform.position + directionToTarget * _speed * Time.fixedDeltaTime);
    }
}
