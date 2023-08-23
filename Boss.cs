using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioSource bossSound;
    public AudioClip bossCry;

    private void Start()
    {
        bossSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
   
    }

    public IEnumerator BossAudio()
    {
        bossSound.Play();

        yield return new WaitForSeconds(3.5f);

        bossSound.clip = bossCry;
        bossSound.loop = true;
        bossSound.Play();
    }
}
