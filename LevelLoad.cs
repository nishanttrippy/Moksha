using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    // Update is called once per frame
    //public Button playButton;
    private Animator startGame;

    private void Start()
    {
        //Button btn = playButton.GetComponent<Button>();
        //btn.onClick.AddListener(TaskOnClick);

        startGame = GetComponent<Animator>();
    }

    public void TaskOnClick()
    {
        StartCoroutine(StartGame());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator StartGame()
    {
        startGame.SetTrigger("StartGame");

        yield return new WaitForSeconds(4.5f);

        SceneManager.LoadScene(1);
    }
}
