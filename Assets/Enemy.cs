using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minDistance = 10f; 
    [SerializeField] private float knockbackForce = 1000f;
    [SerializeField] private float knockbackCooldown = 1f;
    private Player playerScript;
    private int damageAmount = 20;

    private bool isKnockbackCooldown = false;
    private float knockbackCooldownTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnockbackCooldown)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position); // Calculate distance between enemey and player

            if (distanceToPlayer < minDistance) // Check if distance is less than minimum
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // Move towards the player
            }
        }
        else 
        {
            knockbackCooldownTimer -= Time.deltaTime;
            if (knockbackCooldownTimer <= 0f)
            {
                isKnockbackCooldown = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) // Check if the collision is made with player
        {
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized; // Calculate direction from enemy to player
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>(); // Apply force to player
            playerRigidbody.AddForce(knockbackDirection * knockbackForce); // Apply force to player

            isKnockbackCooldown = true;
            knockbackCooldownTimer = knockbackCooldown;

            //Disable player's movement for a second
            PlayerMovement.instance.DisableMovementForCooldown();

            playerScript.TakeDamage(damageAmount);
        }

    }
}
