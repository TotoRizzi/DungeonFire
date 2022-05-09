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

    bool hasEnteredTheCollider;

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

    private void Update()
    {
        if (hasEnteredTheCollider) ShowCanvas();       
        else HideCanvas();
    }

    private void OnTriggerEnter(Collider other)
    {
        hasEnteredTheCollider = true;
        ShowCanvas();
    }
    private void OnTriggerExit(Collider other)
    {
        hasEnteredTheCollider = false;
        HideCanvas();
    }
    private void ShowCanvas()
    {
        if (confirmCanvas.activeInHierarchy == true) return;

        confirmLevelButton.SetTeleportingStar(this);
        confirmCanvas.SetActive(true);
    }
    public void HideCanvas()
    {
        confirmCanvas.SetActive(false);
        hasEnteredTheCollider = false;
    }
}
