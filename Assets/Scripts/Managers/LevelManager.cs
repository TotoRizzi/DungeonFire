using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public string[] newLevelOrder;
    public int levelDifficulty;
    public int currentLevel;
    
    [SerializeField] List<string> _allLevels = new List<string>();

    private int _maxLevels = 5;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        newLevelOrder = new string[_maxLevels];
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
        if (currentLevel < newLevelOrder.Length)
        {
            GameManager.instance.SaveData();
            //LoadScene(newLevelOrder[currentLevel--]);
            Debug.Log((newLevelOrder[currentLevel--]));
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
