using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LvlToGo
{
    LevelOne,
    NextLevel
}
public class TeleportingStar : MonoBehaviour
{
    public LvlToGo lvlToGo;
    [SerializeField] ConfirmLevelButton confirmLevelButton;
    [SerializeField] GameObject confirmCanvas;
    [SerializeField] TextMeshProUGUI text;

    string lvlNumberText;

    bool hasEnteredTheCollider;

    private void Awake()
    {
        switch (lvlToGo)
        {
            case LvlToGo.LevelOne:

                lvlNumberText = "Ready to start?";
                text.text = lvlNumberText;
                break;
            case LvlToGo.NextLevel:
                lvlNumberText = "Next Level?";
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
    }
    private void OnTriggerExit(Collider other)
    {
        hasEnteredTheCollider = false;
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
