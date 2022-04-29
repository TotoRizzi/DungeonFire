using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBullet : Bullet
{
    IMovement forwardMovement;

    private void Awake()
    {
        forwardMovement = new ForwardMovement(speed, transform);
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

    public static void turnOn(ArcherBullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void turnOff(ArcherBullet b)
    {
        b.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        ArcherBullet_Factory.instance.ReturnBullet(this);
    }
}
