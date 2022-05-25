using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSlimeAttack : MonoBehaviour
{
    [SerializeField] float resetAttackColliderTime;

    IDamageable player;
    BoxCollider myCollider;

    [SerializeField] FSMEnemy _myEnemy;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<IDamageable>();       
        
        if(player != null)
            player.TakeDamage(_myEnemy.attackDmg, _myEnemy.transform.position);
       
        myCollider.enabled = false;
        StartCoroutine(DealDamage());    
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(resetAttackColliderTime);
        myCollider.enabled = true;
    }
}
