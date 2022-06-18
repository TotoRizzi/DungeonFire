using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnCollision : MonoBehaviour
{
    [SerializeField] float _attackDmg;
    private void OnTriggerEnter(Collider other)
    {
        var dmgToPlayer = other.GetComponent<IDamageable>();

        if (dmgToPlayer != null)
            dmgToPlayer.TakeDamage(_attackDmg, new Vector3(0,0,transform.position.z));
    }
}
