using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBullet_Factory : MonoBehaviour
{
    public static ArcherBullet_Factory instance;
    public ObjectPool<ArcherBullet> pool;

    public ArcherBullet ArcherBulletPrefab;

    [SerializeField] int maxBulletCreated = 10;

    private void Awake()
    {
        //Lo hago singleton
        if (instance == null)
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
        pool = new ObjectPool<ArcherBullet>(BulletCreator, ArcherBullet.turnOn, ArcherBullet.turnOff, maxBulletCreated);
    }

    public ArcherBullet BulletCreator()
    {
        //La logica para crear el objeto
        return Instantiate(ArcherBulletPrefab);
    }

    public void ReturnBullet(ArcherBullet b)
    {
        //Le devolvemos el objeto cuando sea necesario, lo llamamos desde el script PlayerBasicBullet
        pool.ReturnObject(b);
    }
}
