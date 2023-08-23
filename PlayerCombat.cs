using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator playerAnim;
    private PlayerMove pm;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    //Shooting Params
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float lastShot;
    private float shotCooldown = 3.0f;

    //Melee Attack Params
    private float attackCooldown = 2.0f;
    private float lastSwing;
    public bool isAttacking = false;
    public int attackDamage = 50;

    private void Start()
    {
        pm = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSwing > attackCooldown)
        {
            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time - lastSwing > attackCooldown && !pm.isDashing)
            {
                //Attack Anim
                Attack();
                isAttacking = true;
                lastSwing = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastShot > shotCooldown && !pm.isDashing)
            {
                Shoot();
                lastShot = Time.time;
            }
        }
    }

    void Attack()
    {
        playerAnim.SetTrigger("Attack");

        if (GameManager.instance.hasBossBattleStarted)
        {
            Collider2D hitBoss = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Boss"));

            if (hitBoss != null)
            {
                hitBoss.GetComponent<BossHealth>().TakeDamage(attackDamage);
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Actor"));

            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy enemyComp = enemy.GetComponent<Enemy>();

                if (enemyComp != null)
                {
                    enemyComp.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
