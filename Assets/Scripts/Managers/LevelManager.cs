using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public int[] newLevelOrder;
    public int currentLevel;
    
    [SerializeField] List<int> _allLevels = new List<int>();

    private int _maxLevels = 4;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        newLevelOrder = new int[_maxLevels];
    }
    private void Start()
    {
    }
    public void SetNewOrderOfLevels()
    {
        if (_maxLevels == 0) _maxLevels = _allLevels.Count;

        for (int i = 0; i < _maxLevels; i++)
        {
            int _randomLevel = Random.Range(0, _allLevels.Count);

            newLevelOrder[i] = _allLevels[_randomLevel];

            _allLevels.Remove(_allLevels[_randomLevel]);
        }
        currentLevel = 0;
        GameManager.instance.SaveData();
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel <= newLevelOrder.Length)
        {
            int fixedCurrrentLevel = currentLevel - 1;
            GameManager.instance.SaveData();
            LoadScene("level " + newLevelOrder[fixedCurrrentLevel]);
            Debug.Log((newLevelOrder[fixedCurrrentLevel]));
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
