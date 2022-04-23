using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float maxDistance = 3;
    [SerializeField] protected float bulletDamage;
    protected float currentDistance = 0;

    public virtual void ShotCd()
    {
        currentDistance += Time.deltaTime;
    }

    //El reset es protected porque se llama desde los hijos
    protected void Reset()
    {
        currentDistance = 0;
    }
}
