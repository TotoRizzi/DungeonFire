using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBullet : Bullet
{
    IMovement forwardMovement;

    [SerializeField] float bulletDamage;
    
    [SerializeField] Transform player;

    private void Awake()
    {
        forwardMovement = new ForwardMovement(speed, transform);
        player = GameObject.Find("Player").transform;
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
            //Logica para apagar la bala desde el factory
            PlayerBasicBullet_Factory.instance.ReturnBullet(this);
        }
    }

    //Logica para prender el objeto cuando se llama desde el Factory
    public static void turnOn(PlayerBasicBullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }
    //Logica para apagar el objeto cuando se llama desde el Factory
    public static void turnOff(PlayerBasicBullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(bulletDamage, player.position);
        }

        PlayerBasicBullet_Factory.instance.ReturnBullet(this);
    }
}
