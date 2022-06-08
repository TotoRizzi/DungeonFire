using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWonButtons : MonoBehaviour
{
    public void NextLevel()
    {
        GameManager.instance.myLevelManager.NextLevel();
    }

    public void Menu()
    {
        GameManager.instance.GoToMenu();
    }
}
