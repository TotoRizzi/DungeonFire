using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackOnAnimator : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;

    private void Awake()
    {
        shootingPoint = GameObject.Find("ShootingPoint").transform;
    }
    public void Attack()
    {
        if (shootingPoint != null)
        {
            PlayerBasicBullet_Factory.instance.pool.GetObject().SetPosition(shootingPoint.position).SetRotation(transform.rotation).SetPlayerDamage(GameManager.instance.player);
            SoundManager.instance.PlaySound(Sounds.playerBullet);
        }

    }
}
