using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int maxhealth = 100;
	public int currentHealth;

	public Animator playerAnim;
	public PlayerHealthBar healthBar;
	private AudioSource audioSource;

	public AudioClip heartBeatMedium;
	public AudioClip heartBeatNormal;
	
    private void Start()
    {
		currentHealth = maxhealth;
		healthBar.SetMaxHealth(maxhealth);
		audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
	{
		if (!GetComponent<PlayerMove>().isDashing && currentHealth >= 0)
		{
			currentHealth -= damage;
			healthBar.SetHealth(currentHealth);
			playerAnim.SetTrigger("Hit");
		}

		if (currentHealth <= 0)
		{
			Die();
		}

		if(currentHealth <= 60)
        {
			audioSource.clip = heartBeatNormal;
			audioSource.loop = true;

			audioSource.Play();
        }

		if(currentHealth <= 40)
        {
			audioSource.clip = heartBeatMedium;
			audioSource.loop = true;

			audioSource.Play();
		}
	}

	void Die()
	{
		playerAnim.SetBool("isDead", true);
		GameManager.instance.GameOver();
	}
}
