using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject creditsScreen;

    public void Pause()
    {
        Time.timeScale = 0;
        menuScreen.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
    }
    public void Menu()
    {
        GameManager.instance.GoToMenu();
    }
    public void Credits()
    {
        creditsScreen.SetActive(true);
    }
    public void Quit()
    {

    }

    public void ResetPoints()
    {
        GameManager.instance.ResetPoints();
    }

    public void Back()
    {
        creditsScreen.SetActive(false);
    }
}
