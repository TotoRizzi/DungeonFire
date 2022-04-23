using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : IMovement
{
    float speed;
    Transform transform;

    public ForwardMovement(float _speed, Transform _transform)
    {
        speed = _speed;
        transform = _transform;
    }
    public void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
