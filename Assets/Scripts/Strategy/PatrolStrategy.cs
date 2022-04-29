using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStrategy
{
    Transform myTransform;

    float speed;

    public PatrolStrategy(Transform _myTransform, float _speed)
    {
        myTransform = _myTransform;
        speed = _speed;
    }

    public void Move(Vector3 wayPoint)
    {
        Vector3 dir = wayPoint - myTransform.position;
        dir.y = myTransform.position.y;
        dir.Normalize();

        myTransform.position += dir * speed * Time.deltaTime;
    }
}
