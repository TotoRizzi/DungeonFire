using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : IMovement
{
    float _speed;
    Transform _transform;
    Rigidbody _rb;

    public ForwardMovement(float speed, Transform transform, Rigidbody rb)
    {
        _speed = speed;
        _transform = transform;
        _rb = rb;
    }
    public void Move()
    {
        //_transform.position += _transform.forward * _speed * Time.deltaTime;
        _rb.MovePosition(_transform.position + _transform.forward * _speed * Time.fixedDeltaTime);
    }
}
