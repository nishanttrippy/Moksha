using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Angel;
    public GameObject backgroundAudio;
    public GameObject Boss;
    public GameObject BossHealthBar;
    public GameObject BossArea;
    public GameOverScreen GameOverScreen;

    public bool hasBossBattleStarted = false;

    public AudioClip BossBattleClip;
    public AudioClip AmbientBG;


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAngel()
    {
        Instantiate(Angel, new Vector2(-5.36f, -15.45f), Quaternion.identity, transform.parent);
    }

    public void GameOver()
    {
        GameOverScreen.Setup();
        AudioSource src = backgroundAudio.GetComponent<AudioSource>();
        src.Stop();
    }
    public void BeginBossBattle()
    {
        BossHealthBar.SetActive(true);
        Boss.SetActive(true);
        BossArea.SetActive(true);
        hasBossBattleStarted = true;

        //Boss Battle Audio
        AudioSource src = backgroundAudio.GetComponent<AudioSource>();
        src.clip = BossBattleClip;
        src.Play();

        StartCoroutine(Boss.GetComponent<Boss>().BossAudio());
    }

    public void EndBossBattle()
    {
        BossHealthBar.SetActive(false);
        Boss.SetActive(false);
        BossArea.SetActive(false);

        AudioSource src = backgroundAudio.GetComponent<AudioSource>();
        src.clip = AmbientBG;
        src.Play();
    }
}
