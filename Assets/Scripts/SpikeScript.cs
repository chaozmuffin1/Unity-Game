using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PhysicsMaterial2D hitMaterial;
    [SerializeField] private PhysicsMaterial2D defaultMaterial;

    [SerializeField] private float knockbackForce = 500f;
    [SerializeField] private float knockbackCooldown = 1f;

    public Player playerScript;
    public PlayerMovement playerMovementScript;

    private int damageAmount = 50;

    private bool isKnockbackCooldown = false;
    private float knockbackCooldownTimer = 0f;

    public Rigidbody2D rb;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        knockbackCooldownTimer -= Time.deltaTime;
            if (knockbackCooldownTimer <= 0f)
            {
                isKnockbackCooldown = false;
            }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) // Check if the collision is made with player
        {
            playerMovementScript.rb.sharedMaterial = hitMaterial;
            anim.SetTrigger("hit"); // Play hit animation

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
