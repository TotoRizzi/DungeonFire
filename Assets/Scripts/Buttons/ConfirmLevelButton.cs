using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmLevelButton : MonoBehaviour
{
    TeleportingStar thisStar;

    public void Yes()
    {
        SceneManager.LoadScene(thisStar.sceneToGo);

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
