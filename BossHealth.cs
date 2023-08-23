using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int health;

	//public GameObject deathEffect;
	private Animator bossAnimator;
	private SpriteRenderer bossSprite;

	public bool isInvulnerable = false;

	public static bool isSecondPhaseActive = false;
	public static bool isThirdPhaseActive = false;

    private void Start()
    {
		health = maxHealth;
		bossAnimator = GetComponent<Animator>();
		bossSprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;
		bossAnimator.SetTrigger("Hit");

		if(health <= maxHealth / 2 && !isSecondPhaseActive)
        {
			bossAnimator.SetBool("isSecondPhaseActive", true);
			bossSprite.color = new Color(255, 185, 185);
			BossCombat.attackDamage += 10;
			isSecondPhaseActive = true;
        }
		
		if (health <= maxHealth / 3 && !isThirdPhaseActive)
		{
			bossAnimator.SetBool("isThirdPhaseActive", true);
			bossSprite.color = new Color(255, 0, 0);
			BossCombat.attackDamage += 10;
			BossCombat.castDamage += 10;
			isThirdPhaseActive = true;
		}
		

		if (health <= 0)
		{
			bossAnimator.SetBool("isDead", true);
			Die();
		}
	}

	void Die()
	{ 
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		GameManager.instance.EndBossBattle();
		Destroy(gameObject);
	}

}
