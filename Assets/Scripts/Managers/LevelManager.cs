using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager 
{    
    public int[] newLevelOrder;    

    public LevelManager()
    {
        newLevelOrder = new int[4];
    }

    public void SetNewOrderOfLevels()
    {
        int allLevels = GameManager.instance.allLevels.Count;
        for (int i = 0; i < allLevels; i++)
        {
            int _randomLevel = Random.Range(0, GameManager.instance.allLevels.Count);

            newLevelOrder[i] = GameManager.instance.allLevels[_randomLevel];

            GameManager.instance.allLevels.Remove(GameManager.instance.allLevels[_randomLevel]);
        }
        GameManager.instance.currentLevel = 0;
        GameManager.instance.SaveData();
    }

    public void NextLevel()
    {
        int fixedCurrentLevel = GameManager.instance.currentLevel;
        GameManager.instance.currentLevel++;
        
        if (fixedCurrentLevel < newLevelOrder.Length)
        {
            GameManager.instance.SaveData();
            LoadScene("level " + newLevelOrder[fixedCurrentLevel]);
            Debug.Log((newLevelOrder[fixedCurrentLevel]));
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
