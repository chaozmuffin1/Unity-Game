using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float knockbackForce = 15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // continue video at 7:32
        // Damage enemies in range of attack
        foreach(Collider2D enemy in hitEnemies)
        {
            // Get the enemy's rigidbody
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

            // Calculate the knockback direction (from enemy to player)
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;

            // Apply knockback force to the enemy
            enemyRigidbody.velocity = knockbackDirection * knockbackForce;

            // Damage the enemy
            enemy.GetComponent<Enemy>().TakeDamage(5);

            // Disable the enemy's movement for a second
            enemy.GetComponent<Enemy>().DisableMovementForCooldown();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
