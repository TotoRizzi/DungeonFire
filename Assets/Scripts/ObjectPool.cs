using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    //PRUEBA A VER SI FUNCIONA GITHUB
    Func<T> creationLogic;

    List<T> currentStock; 
    bool isDynamic; 

    Action<T> turnOnCallback; 
    Action<T> turnOffCallback; 

    //Hacemos una funcion del mismo script para que se pueda crear siempre que sea necesario. Al no estar en escena, se requiere esto
    //Pedimos por parametro(cuando es creado) que nos pasen todas las variables que necesita para funcionar
    public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
    {
        creationLogic = factoryMethod;
        this.turnOnCallback = turnOnCallback;
        this.turnOffCallback = turnOffCallback;

        this.isDynamic = isDynamic;

        //Creamos la lista con la cantidad de stock que nos pasaron con la manera de crear que nos pasaron
        currentStock = new List<T>();

        for (int i = 0; i < initialStock; i++)
        {
            T obj = creationLogic();

            this.turnOffCallback(obj);

            currentStock.Add(obj);
        }
    }
    // Van a usar esta funcion cuando nos pidan un objeto
    public T GetObject()
    {
        var result = default(T); 

        if (currentStock.Count > 0)
        {
            result = currentStock[0]; 
            currentStock.RemoveAt(0); 
        }
        else if (isDynamic) 
        {
            result = creationLogic(); 
        }

        turnOnCallback(result); 

        return result;
    }
    //Cuando nos devuelvan el objeto van a llamar a esta funcion
    public void ReturnObject(T obj)
    {
        turnOffCallback(obj);
        currentStock.Add(obj);
    }
}
