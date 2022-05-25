using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected FSMEnemy _myEnemy;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxDistance = 3;
    protected float currentDistance = 0;
    protected float dmg;

    public virtual void ShotCd()
    {
        currentDistance += Time.deltaTime;
    }

    //El reset es protected porque se llama desde los hijos
    protected void Reset()
    {
        currentDistance = 0;
    }

    public Bullet SetPosition(Vector3 position)
    {
        transform.position = position;
        return this;
    }
    public Bullet SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        return this;
    }

    public Bullet SetEnemyDamage(FSMEnemy myEnemy)
    {
        dmg = myEnemy.attackDmg;
        _myEnemy = myEnemy;
        return this;
    }
    public Bullet SetPlayerDamage(Player player)
    {
        dmg = player.attackDmg;
        return this;
    }
}
