using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedButtons : MonoBehaviour
{
    public void Menu()
    {
        GameManager.instance.GoToMenu();
    }
    public void Revive()
    {
        GameManager.instance.Revive();
    }
}
