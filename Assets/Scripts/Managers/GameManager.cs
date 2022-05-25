using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    List <FSMEnemy> currentEnemies = new List<FSMEnemy>();

    [SerializeField] float timeToRestart;

    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelWonMenu;
    [SerializeField] GameObject playerCanvas;

    public Player player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

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

    public void Retry()
    {
        levelFailedMenu.SetActive(false);
        playerCanvas.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
