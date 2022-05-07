using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootInAnimator : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    public void Shoot()
    {
        ArcherBullet_Factory.instance.pool.GetObject().SetPosition(shootingPoint.position).SetRotation(transform.rotation);
    }
}
