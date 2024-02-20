using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth = 20;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerObject;
    public FloatingHealthBar healthBar;
    private int damageAmount = 20;
    public Player playerScript;
    public PlayerMovement playerMovementScript;
    public Animator anim;
    [SerializeField] private GameObject patrollingEnemy;
    [SerializeField] private float knockbackForce;

    public bool isKnockbackCooldown = false;
    public float knockbackCooldownTimer = 0f;
    [SerializeField] private float knockbackCooldown = 1f;

    void Start(){
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        anim = player.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(patrollingEnemy);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == playerObject) // Check if the collision is made with player
        {
            anim.SetTrigger("hit"); // Play hit animation

            Vector2 knockbackDirection = (playerObject.transform.position - transform.position).normalized; // Calculate direction from enemy to player
            Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>(); // Apply force to player
            playerRigidbody.AddForce(knockbackDirection * knockbackForce); // Apply force to player

            Rigidbody2D enemyrb = patrollingEnemy.GetComponent<Rigidbody2D>();
            Vector2 knockbackDirectionEnemy = (transform.position - playerObject.transform.position).normalized;
            enemyrb.AddForce(knockbackDirectionEnemy * (knockbackForce/5));

            isKnockbackCooldown = true;
            knockbackCooldownTimer = knockbackCooldown;


            //Disable player's movement for a second
            PlayerMovement.instance.DisableMovementForCooldown();

            playerScript.TakeDamage(damageAmount);
        }
    }
}
