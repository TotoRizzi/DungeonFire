using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage;

    IDamageable dmgToPlayer;

    private void OnTriggerEnter(Collider other)
    {
        dmgToPlayer = other.GetComponent<IDamageable>();       
        
        if(dmgToPlayer != null)
            dmgToPlayer.TakeDamage(damage, transform.position);
    }
}
