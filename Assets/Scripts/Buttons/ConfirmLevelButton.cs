using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmLevelButton : MonoBehaviour
{
    TeleportingStar thisStar;

    public void Yes()
    {
        if (thisStar.lvlToGo == LvlToGo.LevelOne)
        {
            GameManager.instance.ClearLevelOrderData();
            GameManager.instance.myLevelManager.SetNewOrderOfLevels();
        }

        GameManager.instance.myLevelManager.NextLevel();
    }
    public void No()
    {
        thisStar.HideCanvas();
    }

    public void SetTeleportingStar(TeleportingStar myStar)
    {
        thisStar = myStar;
    }
}
