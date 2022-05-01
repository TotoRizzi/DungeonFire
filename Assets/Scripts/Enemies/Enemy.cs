using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Player player;

    protected bool canMove = true;
    
    #region MovementVariables

    [SerializeField] protected float speed;
    [SerializeField] protected float sightRange;
    [SerializeField] protected float shootRange;
    #endregion

    #region KnockbackVariables

    [SerializeField] protected float knockbackForce;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected float resetTime;
    
    protected float currentKnockbackDuration;
    protected KnockBackStrategy knockBack;
    protected bool isInKnockback;

    Vector3 lastDamageDealer;

    #endregion

    public virtual void Awake()
    {
        player = FindObjectOfType<Player>();
        knockBack = new KnockBackStrategy(transform, knockbackForce);
    }
    
    public virtual void Update()
    {
        Knockback();
    }

    public void SetDamageDealer(Vector3 dmgDealer)
    {
        //BUG NO FUNCIONA NO SE PORQUE lastDamageDealer = dmgDealer;   

        lastDamageDealer = player.transform.position;
    }

    public void SetKnockBackToTrue()
    {
        isInKnockback = true;
    }

    public virtual void LookAtPlayer()
    {
        //transform.forward = player.transform.position - transform.position;
        Vector3 dirToLook = player.transform.position;
        dirToLook.y = transform.position.y;
        transform.LookAt(dirToLook);
    }

    protected Vector3 GetVectorToPlayer()
    {
        Vector3 dis = player.transform.position - transform.position;
        dis.y = transform.position.y;
        dis.Normalize();

        return dis;
    }

    public virtual void Knockback()
    {
        if (isInKnockback)
        {
            if (currentKnockbackDuration < knockbackDuration)
            {
                currentKnockbackDuration += Time.deltaTime;
                knockBack.SetVariables(lastDamageDealer);
                knockBack.Move();
                canMove = false;
            }
            else
            {
                isInKnockback = false;
                currentKnockbackDuration = 0;

                StartCoroutine(ResetTime(resetTime));
            }
        }
    }

    protected bool SeePlayer(Vector3 seePoint)
    {
        Vector3 vectorToPlayer = (player.transform.position - transform.position);
        vectorToPlayer.y = transform.position.y;

        RaycastHit ray;

        if (Physics.Raycast(seePoint, vectorToPlayer.normalized, out ray, sightRange, (1 << LayerMask.NameToLayer("PlayerHitBox") | (1 << LayerMask.NameToLayer("Wall")))))
        {
            if (ray.transform.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        canMove = true;
    }


}
