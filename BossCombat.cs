using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
	static public int attackDamage = 20;
	static public int castDamage = 30;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	//Projectile Attack
	public GameObject[] castSprits = new GameObject[8];
	float radius, moveSpeed;

	//Homing Spirit Attack
	public GameObject HomingSpirit;
	public Transform homingSource;

    private void Start()
    {
		radius = 10f;
		moveSpeed = 1.2f;
    }

    public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
	}

	public void CastAttackHandler(Animator animator)
    {
		StartCoroutine(CastAttackTimer(animator));
    }

	IEnumerator CastAttackTimer(Animator animator)
	{
		yield return new WaitForSeconds(0.5f);

		int attackChoice = Random.Range(1, 2);
		switch (attackChoice)
		{
			case 1:
				if (BossHealth.isSecondPhaseActive)
				{
					animator.SetTrigger("RangedAttack1");
				}
				break;

			case 2:
				if (BossHealth.isThirdPhaseActive)
				{
					animator.SetTrigger("RangedAttack2");
				}
				break;
		}
	}

	public void MultiCastAttack()
    {
		Vector2 castStartPoint = GetComponent<Transform>().position;

		float angleStep = 360f / castSprits.Length;
		float angle = 0f;

		for (int i = 0; i <= castSprits.Length - 1; i++)
		{

			float projectileDirXposition = castStartPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = castStartPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - castStartPoint).normalized * moveSpeed;

			Instantiate(castSprits[i], castStartPoint, Quaternion.identity);

			castSprits[i].GetComponent<CastSpirits>().SetSpiritParams(projectileMoveDirection);

			angle += angleStep;
		}
	}

	public void HomingAttack()
    {
		Instantiate(HomingSpirit, homingSource.position, transform.rotation);
    }

    void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
