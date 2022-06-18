using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FSMEnemy : MonoBehaviour , IDie
{
    
    protected Rigidbody rb;
    protected Player player;

    [HideInInspector] public StateMachine fsm;
    [HideInInspector] public Animator anim;
    [HideInInspector] public FSMEnemyHealth enemyHealth;
    [HideInInspector] public IMovement myMovement;

    [Header("FSM Enemy variables")]
    public float attackDmg;

    protected bool canMove = true;
    protected bool isAlive = true;

    private BoxCollider _myCollider;
    private float _knockbackForce = 4f;
    private float _resetTime = .3f;

    protected int ponitsToGive;

    public virtual void Awake()
    {
        fsm = new StateMachine();
        

        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        _myCollider = GetComponent<BoxCollider>();

        anim = GetComponentInChildren<Animator>();
    }
    public virtual void Start()
    {
        GameManager.instance.AddEnemy(this);
    }
    public virtual void Update()
    {
        if(canMove) fsm.Update();
    }
    public bool SeePlayer()
    {
        Vector3 vectorToPlayer = (player.transform.position - transform.position);
        vectorToPlayer.y = 1;

        RaycastHit ray;
        if (Physics.Raycast(transform.position, vectorToPlayer, out ray, FlyweightPointer.AllEnemies.sightRange, (1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Wall")))))
        {
            if (ray.transform.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    public void LookAtPlayer()
    {
        Vector3 dirToLook = player.transform.position;
        dirToLook.y = transform.position.y;
        transform.LookAt(dirToLook);
    }

    public void Knockback()
    {
        canMove = false;

        rb.AddForce(-transform.forward * _knockbackForce, ForceMode.Impulse);

        StartCoroutine(ResetTime(_resetTime));
    }

    private IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        if (isAlive) canMove = true;
    }

    public virtual void Die()
    {
        if (anim != null) anim.SetTrigger("die");
        else Destroy(this.gameObject);

        GameManager.instance.AddPoints(ponitsToGive);
        GameManager.instance.RemoveEnemy(this);

        canMove = false;
        isAlive = false;
        rb.isKinematic = true;

        _myCollider.enabled = false;
    }


    public float GetDistanceToPlayer()
    {
        Vector3 directionToTarget;
        directionToTarget = player.transform.position - transform.position;
        directionToTarget = new Vector3(directionToTarget.x, 1, directionToTarget.z);

        return directionToTarget.magnitude;
    }
}
