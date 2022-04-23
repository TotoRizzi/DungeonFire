using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMovement
{
    [SerializeField] Controller moveController;
    [SerializeField] Controller lookAndFireController;

    Vector3 dirToMove;
    Vector3 dirToLook;
    Vector3 velocity;

    [SerializeField] float maxSpeed;
    [SerializeField] float maxMovingForce;
    [SerializeField] float ShotCd = .3f;
    float currentShotCd = 0;

    [SerializeField] Transform shootingPoint;
    private void Update()
    {
        Move();
        Look();
        Debug.Log("Funciona");
    }
    public void Move()
    {
       
        dirToMove = moveController.GetDir();
        dirToMove.Normalize();
        dirToMove *= maxSpeed;
       
        Vector3 steering = dirToMove - velocity;
        steering = Vector3.ClampMagnitude(steering, maxMovingForce);
        ApplyForce(steering);

        transform.position += velocity * Time.deltaTime;
        if (velocity == Vector3.zero)
            return;
        transform.forward = velocity;
       
        /*
        if (dirToMove != Vector3.zero)
        {
            ApplyForce(dirToMove);
            transform.position += velocity * Time.deltaTime;
            currentMoveVelocity = dirToMove;
        }
        else
        {
            ApplyForce(-currentMoveVelocity);
            currentMoveVelocity = Vector3.zero;
        }
        */
        
    }

    private void ApplyForce(Vector3 force)
    {
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    private void Look()
    {
        if (lookAndFireController.GetDir() != Vector3.zero)
        {
            dirToLook = lookAndFireController.GetDir();

            transform.forward = dirToLook;

            Shoot();
        }
        else
        {
            dirToLook = moveController.GetDir();
        }
    }
    private void Shoot()
    {
        currentShotCd += Time.deltaTime;

        if (currentShotCd >= ShotCd)
        {
            //Crea las balas llamandolas al factory

            var b = PlayerBasicBullet_Factory.instance.pool.GetObject();
            b.transform.position = shootingPoint.position;
            b.transform.rotation = transform.rotation;
            currentShotCd = 0;
        }
    }
}
