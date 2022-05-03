using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInAnimator : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;

    public void Attack()
    {
        PlayerBasicBullet_Factory.instance.pool.GetObject().SetPosition(shootingPoint.position).SetRotation(transform.rotation);
    }
}
