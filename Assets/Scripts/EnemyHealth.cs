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

            Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>(); // Apply force to player

            //Disable player's movement for a second
            PlayerMovement.instance.DisableMovementForCooldown();

            Debug.Log("collision detected");
            playerScript.TakeDamage(damageAmount);
        }
    }
}
