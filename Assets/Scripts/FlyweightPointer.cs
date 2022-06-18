using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlyweightPointer
{
    public static readonly Flyweight Slime = new Flyweight
    {
        speed = 4,
        maxHealth = 3,
        
        jumpForce = 4
    };
    public static readonly Flyweight BladeBlade = new Flyweight
    {
        speed = 8,
        maxHealth = 5,

        jumpForce = 5
    };
    public static readonly Flyweight Skeleton = new Flyweight
    {
        speed = 1,
        maxHealth = 3
    };
    public static readonly Flyweight Golem = new Flyweight
    {
        speed = 1,
        maxHealth = 10
    };
    public static readonly Flyweight AllEnemies = new Flyweight
    {
        sightRange = 20
    };
}
