using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float resetAttackColliderTime;

    IDamageable dmgToPlayer;
    BoxCollider myCollider;

    private enum EnemyAttackType
    {
        Bullet,
        Slime
    }
    [SerializeField] EnemyAttackType attackType; 

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        dmgToPlayer = other.GetComponent<IDamageable>();       
        
        if(dmgToPlayer != null)
            dmgToPlayer.TakeDamage(damage, transform.position);
       
        switch (attackType)
        {
            case EnemyAttackType.Slime:
                myCollider.enabled = false;
                StartCoroutine(DealDamage());
                break;
            default:
                break;
        }
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(resetAttackColliderTime);
        myCollider.enabled = true;
    }
}
