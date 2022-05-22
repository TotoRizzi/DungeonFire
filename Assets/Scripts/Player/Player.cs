using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDie
{
    #region Movement
    public Controller moveController;
    public Controller lookAndFireController;

    PlayerController controller;

    Vector3 velocity;

    [SerializeField] float maxSpeed;
    [SerializeField] float maxMovingForce;

    [SerializeField] Rigidbody rb;

    public bool canMove = true;
    bool isDead;
    #endregion

    #region Shooting

    [SerializeField] float shotCd = .3f;
    float currentShotCd;

    #endregion

    #region KnockBack

    KnockBackStrategy knockBack;

    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;
    [SerializeField] float resetTime;
    float currentKnockbackDuration;

    Vector3 lastDamageDealer;

    bool isInKnockback;
    bool fixedKnockBackForAnimation;
    #endregion

    #region PlaterView

    public event Action<Vector3> onMovement = delegate { };
    public event Action onKnockBack;
    public event Action onDeath;
    public event Action onShoot;

    #endregion

    [SerializeField] Animator anim;


    private void Awake()
    {
        currentShotCd = shotCd;
        
        knockBack = new KnockBackStrategy(transform, knockbackForce);
        controller = new PlayerController(this, GetComponentInChildren<PlayerView>(), GetComponent<PlayerHealth>());
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.OnUpdate();
        }        
    }
    public void Move(Vector3 _dirToMove)
    {
        _dirToMove *= maxSpeed;

        Vector3 steering = _dirToMove - velocity;
        steering = Vector3.ClampMagnitude(steering, maxMovingForce);
        ApplyForce(steering);

        if (velocity == Vector3.zero)
        {
            onMovement(velocity);
            return;
        }
        
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        transform.forward = velocity;

        onMovement(velocity);
    }

    public void Look(Vector3 _dirToLook)
    {
        if (_dirToLook != Vector3.zero)
        {
            transform.forward = _dirToLook;

            Shoot();
        }
    }

    private void ApplyForce(Vector3 force)
    {
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    private void Shoot()
    {
        currentShotCd += Time.deltaTime;
        if (currentShotCd >= shotCd)
        {
            onShoot();
            currentShotCd = 0;
        }
    }

    public void SetDamageDealer(Vector3 dmgDealer)
    {
        lastDamageDealer = dmgDealer;
    }

    public void Die()
    {
        onDeath();
        canMove = false;
        isDead = true;
        GameManager.instance.LevelFailed();
    }

    public void Revive()
    {
        isDead = false;
        canMove = true;
        anim.SetTrigger("revive");
    }

    public virtual void Knockback()
    {
        if (!fixedKnockBackForAnimation)
        {
            fixedKnockBackForAnimation = true;
            onKnockBack();
        }

        canMove = false;
        knockBack.SetVariables(lastDamageDealer);

        Vector3 lookAtLastDmgDealer = lastDamageDealer - transform.position;
        lookAtLastDmgDealer.y = transform.position.y;

        transform.forward = lookAtLastDmgDealer;

        lookAtLastDmgDealer = lookAtLastDmgDealer.normalized * knockbackForce;
        rb.AddForce(-lookAtLastDmgDealer, ForceMode.Impulse);

        StartCoroutine(ResetTime(resetTime));       
    }

    IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        fixedKnockBackForAnimation = false;
        if(!isDead) canMove = true;
    }
}