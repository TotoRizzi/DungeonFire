using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    playerBullet,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    Sounds sounds;

    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject[] playerBullet;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    { 
    }
    public void PlaySound(Sounds s)
    {
        switch (s)
        {
            case Sounds.playerBullet:
                
                GameObject soundToPlay;
                for (int i = 0; i < playerBullet.Length; i++)
                {
                    if (playerBullet[i].activeInHierarchy == true) continue;
                    else soundToPlay = playerBullet[i];
                    
                    soundToPlay.SetActive(true);
                    StartCoroutine(StopSound(soundToPlay));
                    break;
                }
                break;
        }
    }

    IEnumerator StopSound(GameObject sound)
    {
        yield return new WaitForSeconds(1f);

        sound.SetActive(false);
    }
}
