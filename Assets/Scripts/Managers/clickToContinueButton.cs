using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToContinueButton : MonoBehaviour
{
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
    public void OnClick()
    {
        Debug.Log("Funciono");
        SceneManager.LoadScene("Main Menu");
    }
}
