using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : IMovement
{
    float speed;
    Transform transform;
    Transform target;

    Vector3 directionToTarget;

    public DirectedMovement(float _speed, Transform _transform, Transform _target)
    {
        speed = _speed;
        transform = _transform;
        target = _target;
    }
    public void Move()
    {
        directionToTarget = GetDirectionToTarget().normalized;

        transform.position += directionToTarget * speed * Time.deltaTime;
    }

    public Vector3 GetDirectionToTarget()
    {
        directionToTarget = target.position - transform.position;
        directionToTarget = new Vector3(directionToTarget.x, 0, directionToTarget.z);
        
        return directionToTarget;
    }
}
