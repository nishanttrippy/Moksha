using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
  public void Setup()
    {
        gameObject.SetActive(true);
        StartCoroutine(TimeFreeze());
    }

    public void Respawn()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    IEnumerator TimeFreeze()
    {
        yield return new WaitForSeconds(2.0f);

        Time.timeScale = 0f;
    }
}
