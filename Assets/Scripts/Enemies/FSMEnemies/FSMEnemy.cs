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

    [SerializeField] protected float speed;
    [SerializeField] protected float sightRange = 10f;

    [SerializeField] private float _maxHealth;

    private BoxCollider _myCollider;
    private float _knockbackForce = 4f;
    private float _resetTime = .3f;

    private int _ponitsToGive;

    public virtual void Awake()
    {
        fsm = new StateMachine();
        

        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        _myCollider = GetComponent<BoxCollider>();

        anim = GetComponentInChildren<Animator>();

        enemyHealth = new FSMEnemyHealth(_maxHealth, this);
    }
    public virtual void Start()
    {
        GameManager.instance.AddEnemy(this);
        _ponitsToGive = Mathf.RoundToInt(_maxHealth);
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
        if (Physics.Raycast(transform.position, vectorToPlayer, out ray, sightRange, (1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Wall")))))
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

    public void Knockback(Vector3 dmgDealer)
    {
        canMove = false;

        Vector3 lookAtDmgDealer = dmgDealer - transform.position;
        lookAtDmgDealer.y = transform.rotation.y;

        transform.forward = lookAtDmgDealer;

        lookAtDmgDealer = lookAtDmgDealer.normalized * _knockbackForce;
        rb.AddForce(-lookAtDmgDealer, ForceMode.Impulse);

        StartCoroutine(ResetTime(_resetTime));
    }

    private IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        if (isAlive) canMove = true;
    }

    public void Die()
    {
        if (anim != null) anim.SetTrigger("die");
        else Destroy(this.gameObject);

        GameManager.instance.AddPoints(_ponitsToGive);
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
