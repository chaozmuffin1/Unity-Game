using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public SpriteRenderer playerSpriteRenderer; // Reference to the SpriteRenderer Component

    public Sprite healthySprite;
    public Sprite deadSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void UpdatePlayerSprite()
    {
        if(currentHealth <= 0)
        {
            playerSpriteRenderer.sprite = deadSprite;
        }
        else{
            playerSpriteRenderer.sprite = healthySprite;
        }
    }
}
