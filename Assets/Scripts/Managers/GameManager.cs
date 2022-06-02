using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        LoadData();
    }

    #region Enemy Manager

    List <FSMEnemy> currentEnemies = new List<FSMEnemy>();
    public void AddEnemy(FSMEnemy enemy)
    {
        if(!currentEnemies.Contains(enemy)) 
            currentEnemies.Add(enemy);
    }

    public void RemoveEnemy(FSMEnemy enemy)
    {
        if (currentEnemies.Contains(enemy))
            currentEnemies.Remove(enemy);

        if(currentEnemies.Count == 0)
        {
            LevelWon();
        }
    }

    #endregion

    #region LevelOutcome Manager

    [SerializeField] float timeToRestart;

    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelWonMenu;
    [SerializeField] GameObject playerCanvas;

    void LevelWon()
    {
        player.canMove = false;
        StartCoroutine(ShowWonMenu(timeToRestart));
    }

    public void LevelFailed()
    {
        StartCoroutine(ShowFailedMenu(timeToRestart));
    }

    IEnumerator ShowFailedMenu(float t)
    {
        playerCanvas.SetActive(false);

        yield return new WaitForSeconds(t);

        levelFailedMenu.SetActive(true);    
    }
    IEnumerator ShowWonMenu(float t)
    {
        playerCanvas.SetActive(false);

        yield return new WaitForSeconds(t);

        levelWonMenu.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Retry()
    {
        levelFailedMenu.SetActive(false);
        playerCanvas.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region SaveData

    [HideInInspector] private string _currentLevelPrefsName = "CurrentLevel";
    [HideInInspector] private string _firstLevelPrefsName = "FirstLevel";
    [HideInInspector] private string _secondLevelPrefsName = "SecondLevel";
    [HideInInspector] private string _thirdLevelPrefsName = "ThirdLevel";
    [HideInInspector] private string _fourthLevelPrefsName = "FourthLevel";
    [HideInInspector] private string _fifthLevelPrefsName = "FifthLevel";

    public void SaveData()
    {
        PlayerPrefs.SetInt(_currentLevelPrefsName, LevelManager.instance.currentLevel);

        PlayerPrefs.SetString(_firstLevelPrefsName, LevelManager.instance.newLevelOrder[0]);
        PlayerPrefs.SetString(_secondLevelPrefsName, LevelManager.instance.newLevelOrder[1]);
        PlayerPrefs.SetString(_thirdLevelPrefsName, LevelManager.instance.newLevelOrder[2]);
        PlayerPrefs.SetString(_fourthLevelPrefsName, LevelManager.instance.newLevelOrder[3]);
        PlayerPrefs.SetString(_fifthLevelPrefsName, LevelManager.instance.newLevelOrder[4]);

    }
    private void LoadData()
    {
        LevelManager.instance.currentLevel = PlayerPrefs.GetInt(_currentLevelPrefsName, 0);

        LevelManager.instance.newLevelOrder[0] = PlayerPrefs.GetString(_firstLevelPrefsName, "level 1");
        LevelManager.instance.newLevelOrder[1] = PlayerPrefs.GetString(_secondLevelPrefsName, "level 2");
        LevelManager.instance.newLevelOrder[2] = PlayerPrefs.GetString(_thirdLevelPrefsName, "level 3");
        LevelManager.instance.newLevelOrder[3] = PlayerPrefs.GetString(_fourthLevelPrefsName, "level 4");
        LevelManager.instance.newLevelOrder[4] = PlayerPrefs.GetString(_fifthLevelPrefsName, "level 5");
    }
    #endregion
}
