using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LvlToGo
{
    LevelOne,
}
public class TeleportingStar : MonoBehaviour
{
    [SerializeField] LvlToGo lvlToGo;
    [SerializeField] ConfirmLevelButton confirmLevelButton;
    [SerializeField] GameObject confirmCanvas;
    [SerializeField] TextMeshProUGUI text;

    string lvlNumberText;

    [HideInInspector] public string sceneToGo;

    private void Awake()
    {
        switch (lvlToGo)
        {
            case LvlToGo.LevelOne:

                sceneToGo = "level 1";
                lvlNumberText = "Go to level one?";
                text.text = lvlNumberText;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowCanvas();
    }

    private void ShowCanvas()
    {
        confirmLevelButton.SetTeleportingStar(this);
        confirmCanvas.SetActive(true);
    }
    public void HideCanvas()
    {
        confirmCanvas.SetActive(false);
    }
}
