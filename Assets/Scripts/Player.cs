using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;
    public SpriteRenderer playerSpriteRenderer; // Reference to the SpriteRenderer Component

    
    /*[SerializeField] private bool active = true;
    private bool hasDied = false;*/

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentHealth <= 0)
        {
            Die();
        }
        if (!active)
        {
            return;
        }*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    /*public void Die()
    {
        if (hasDied)
        {
            return;
        }
        hasDied = true;

        active = false;
        transform.Rotate(0,0, -90);

        PlayerMovement.instance.DisableMovementForCooldown();
    }*/

    
}
