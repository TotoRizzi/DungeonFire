using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public PlayerHealth playerHealth;

    public int currentLevel = 0;
    private int maxLevels;
    public LevelManager myLevelManager;
    public List<int> allLevels = new List<int>();

    GameObject teleportingStar;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        myLevelManager = new LevelManager();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        LoadData();
        maxLevels = allLevels.Count;
        SetTeleportingStar();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearLevelOrderData();
            myLevelManager.SetNewOrderOfLevels();
        }
    }

    #region Points

    private int _points;
    [SerializeField] TextMeshProUGUI _pointsText;

    public void AddPoints(int p)
    {
        _points += p;
        _pointsText.text = "Points: " + _points.ToString();
        SavePoints();
    }

    public void ResetPoints()
    {
        _points = 0;
        _pointsText.text = "Points: " + _points.ToString();
        SavePoints();

    }

    #endregion

    #region Enemy Manager

    List<FSMEnemy> currentEnemies = new List<FSMEnemy>();
    public void AddEnemy(FSMEnemy enemy)
    {
        if(!currentEnemies.Contains(enemy)) 
            currentEnemies.Add(enemy);
    }

    public void RemoveEnemy(FSMEnemy enemy)
    {
         Vector3 starNewPos = Vector3.zero;
        if(currentEnemies.Count == 1)
        {
            starNewPos = currentEnemies[0].transform.position;
            starNewPos.y = 1.5f;
        }

        if (currentEnemies.Contains(enemy))
            currentEnemies.Remove(enemy);

        if(currentEnemies.Count == 0)
        {
            if (currentLevel >= maxLevels) GameWon();
            else if (!teleportingStar.activeInHierarchy)
            {
                teleportingStar.SetActive(true);
                teleportingStar.transform.position = starNewPos;
            }
        }
    }

    #endregion

    #region LevelOutcome Manager

    float timeToRestart = 2f;

    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelWonMenu;
    [SerializeField] GameObject playerCanvas;

    void SetTeleportingStar()
    {
        teleportingStar = GameObject.Find("TeleportingStar");
        if(teleportingStar) teleportingStar.SetActive(false);
    }
    void GameWon()
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
    public void Revive()
    {
        levelFailedMenu.SetActive(false);
        playerCanvas.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region SaveData

    private string _currentLevelPrefsName = "CurrentLevel";

    private string _firstLevelPrefsName = "FirstLevel";
    private string _secondLevelPrefsName = "SecondLevel";
    private string _thirdLevelPrefsName = "ThirdLevel";
    private string _fourthLevelPrefsName = "FourthLevel";

    private string _pointsPrefsName = "Points";
    private string _playerHealth = "PlayerHealth";

    public void SavePoints()
    {
        PlayerPrefs.SetInt(_pointsPrefsName, _points);
    }

    public void SavePlayerHealth(float ph)
    {
        PlayerPrefs.SetFloat(_playerHealth, ph);
    }
    public void ClearLevelOrderData()
    {
        PlayerPrefs.DeleteKey(_firstLevelPrefsName);
        PlayerPrefs.DeleteKey(_secondLevelPrefsName);
        PlayerPrefs.DeleteKey(_thirdLevelPrefsName);
        PlayerPrefs.DeleteKey(_fourthLevelPrefsName);

        PlayerPrefs.DeleteKey(_playerHealth);
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt(_currentLevelPrefsName, currentLevel);

        PlayerPrefs.SetInt(_firstLevelPrefsName, myLevelManager.newLevelOrder[0]);
        PlayerPrefs.SetInt(_secondLevelPrefsName, myLevelManager.newLevelOrder[1]);
        PlayerPrefs.SetInt(_thirdLevelPrefsName, myLevelManager.newLevelOrder[2]);
        PlayerPrefs.SetInt(_fourthLevelPrefsName, myLevelManager.newLevelOrder[3]);
    }
    public void LoadData()
    {
        #region Level Manager
        currentLevel = PlayerPrefs.GetInt(_currentLevelPrefsName, 0);

        myLevelManager.newLevelOrder[0] = PlayerPrefs.GetInt(_firstLevelPrefsName, 1);
        myLevelManager.newLevelOrder[1] = PlayerPrefs.GetInt(_secondLevelPrefsName, 2);
        myLevelManager.newLevelOrder[2] = PlayerPrefs.GetInt(_thirdLevelPrefsName, 3);
        myLevelManager.newLevelOrder[3] = PlayerPrefs.GetInt(_fourthLevelPrefsName, 4);

        #endregion

        #region Player

        _points = PlayerPrefs.GetInt(_pointsPrefsName, 0);
        _pointsText.text = "Points: " + _points.ToString();

        playerHealth.currentHealth = PlayerPrefs.GetFloat(_playerHealth, playerHealth.maxHealth);
        playerHealth.ChangeHealthBar();

        #endregion
    }
    #endregion
}
