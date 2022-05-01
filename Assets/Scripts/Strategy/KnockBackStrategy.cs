using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackStrategy : IMovement
{
    Transform myTransform;

    float knockbackForce;
    Vector3 damageDealer;

    public KnockBackStrategy(Transform _myTransform, float _knockbackForce)
    {
        myTransform = _myTransform;
        knockbackForce = _knockbackForce;
    }

    public void SetVariables(Vector3 _damageDealer)
    {
        damageDealer = _damageDealer;
    }

    public void Move()
    {
        Vector3 dir = damageDealer - myTransform.position;
        dir.y = myTransform.position.y;
        dir.Normalize();
        myTransform.position += -dir * knockbackForce * Time.deltaTime;
    }

}
