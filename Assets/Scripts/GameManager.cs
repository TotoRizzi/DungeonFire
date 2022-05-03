using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    [SerializeField] GameObject menuScreen;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


}
