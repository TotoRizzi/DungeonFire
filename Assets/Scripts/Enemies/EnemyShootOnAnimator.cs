using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootOnAnimator : MonoBehaviour
{
    [SerializeField] Transform _shootingPoint;
    [SerializeField] FSMEnemy _myEnemy;
    public void Shoot()
    {
        _myEnemy.LookAtPlayer();
        ArcherBullet_Factory.instance.pool.GetObject().SetPosition(_shootingPoint.position).
                                                       SetRotation(transform.rotation).
                                                       SetEnemyDamage(_myEnemy);
    }
}
