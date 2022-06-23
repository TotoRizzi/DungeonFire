using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class clickToContinueButton : MonoBehaviour
{
    


    public void OnClick()
    {
        GameManager.instance.GoToMenu();
    }
   
}
