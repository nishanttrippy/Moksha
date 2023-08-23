using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingSpirit : MonoBehaviour
{

	private Transform target;

	public float speed = 5.0f;
	public float rotateSpeed = 200.0f;

	private Rigidbody2D rb;

	public GameObject spiritExplosion;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(SelfDestruct());
	}

	void FixedUpdate()
	{
		Vector2 direction = (Vector2)target.position - rb.position;

		//if(direction.x > 0)
  //      {
		//	transform.localScale = new Vector3(-2.0f, 2.0f, 1.0f);
  //      }
		//else
  //      {
		//	transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
  //      }

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Put a particle effect here
		if (other.name == "Protagonist_0")
		{
			Instantiate(spiritExplosion, transform.position, transform.rotation);
			other.GetComponent<PlayerHealth>().TakeDamage(40);
			Destroy(gameObject);
		}
	}

	IEnumerator SelfDestruct()
    {
		yield return new WaitForSeconds(5.0f);

		Instantiate(spiritExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}