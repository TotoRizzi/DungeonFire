using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LvlToGo
{
    LevelOne,
    LevelTwo,
    LevelThree,
    LevelFour
}
public class TeleportingStar : MonoBehaviour
{
    [SerializeField] LvlToGo lvlToGo;
    private string sceneToGo;

    private void Awake()
    {
        switch (lvlToGo)
        {
            case LvlToGo.LevelOne:
                sceneToGo = "level 1";
                    break;
            case LvlToGo.LevelTwo:
                sceneToGo = "level 2";
                break;
            case LvlToGo.LevelThree:
                sceneToGo = "level 3";
                break;
            case LvlToGo.LevelFour:
                sceneToGo = "level 4";
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
