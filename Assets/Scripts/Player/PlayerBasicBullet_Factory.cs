using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBullet_Factory : MonoBehaviour
{
    public static PlayerBasicBullet_Factory instance;
    public ObjectPool<PlayerBasicBullet> pool;

    public PlayerBasicBullet PlayerBasicBulletPrefab;

    [SerializeField] int maxBulletCreated = 10;

    private void Awake()
    {
        //Lo hago singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //Creo el pool
        pool = new ObjectPool<PlayerBasicBullet>(BulletCreator, PlayerBasicBullet.turnOn, PlayerBasicBullet.turnOff, maxBulletCreated);
    }

    public PlayerBasicBullet BulletCreator()
    {
        //La logica para crear el objeto
        return Instantiate(PlayerBasicBulletPrefab);
    }
    
    public void ReturnBullet(PlayerBasicBullet b)
    {
        //Le devolvemos el objeto cuando sea necesario, lo llamamos desde el script PlayerBasicBullet
        pool.ReturnObject(b);
    }
}
