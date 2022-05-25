using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMGolem_AttackOnAnimator : MonoBehaviour
{
    [SerializeField] FSMEnemy _myEnemy;

    private void OnTriggerEnter(Collider other)
    {
        var dmgToPlayer = other.GetComponent<IDamageable>();

        if (dmgToPlayer != null)
            dmgToPlayer.TakeDamage(_myEnemy.attackDmg, transform.position);
    }
}
