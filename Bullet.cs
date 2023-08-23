using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float speed = 3f;
	public int damage = 20;
	private Rigidbody2D rb;
	public GameObject impactEffect;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();
		BossHealth boss = hitInfo.GetComponent<BossHealth>();

		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}

		if(boss != null)
        {
			boss.TakeDamage(damage);
        }
		

		if (hitInfo.name != "Protagonist_0")
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
