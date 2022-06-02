using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedButtons : MonoBehaviour
{
    public void Retry()
    {
        GameManager.instance.Retry();
    }
    public void Menu()
    {
        GameManager.instance.GoToMenu();
    }
}
