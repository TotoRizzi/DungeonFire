using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    IMovement forwardMovement;

    private void Awake()
    {
        forwardMovement = new ForwardMovement(speed, transform, GetComponent<Rigidbody>());
    }
    private void Update()
    {
        forwardMovement.Move();
        ShotCd();
    }
    public override void ShotCd()
    {
        base.ShotCd();
        if (currentDistance >= maxDistance)
        {
            ArcherBullet_Factory.instance.ReturnBullet(this);
        }
    }

    public static void turnOn(EnemyBullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void turnOff(EnemyBullet b)
    {
        b.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        var dmgToPlayer = other.GetComponent<IDamageable>();

        if (dmgToPlayer != null)
            dmgToPlayer.TakeDamage(dmg, _myEnemy.transform.position);

        ArcherBullet_Factory.instance.ReturnBullet(this);
    }
}
