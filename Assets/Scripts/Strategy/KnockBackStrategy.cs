using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackStrategy
{
    Transform myTransform;

    float knockbackForce;

    public KnockBackStrategy(Transform _myTransform, float _knockbackForce)
    {
        myTransform = _myTransform;
        knockbackForce = _knockbackForce;
    }

    public void Move(Vector3 damageDealer)
    {
        Vector3 dir = damageDealer - myTransform.position;
        dir.y = myTransform.position.y;
        dir.Normalize();
        myTransform.position += -dir * knockbackForce * Time.deltaTime;
    }
}
