using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    //the player's max health
    private int maxHealth = 100;
    //the player's current health
    public int currentHealth;
    //reference to the player's health bar game object
    public HealthBar healthBar;
    //reference to the player's sprite renderer
    public SpriteRenderer playerSpriteRenderer; 
    //the death screen game object
    [SerializeField] private GameObject deathScreen = null;
    //the visible health bar game object (for the player)
    [SerializeField] private GameObject healthCanvas = null;
    //the player game object
    [SerializeField] private GameObject player = null;
    //the enemy game object
    [SerializeField] private GameObject enemy = null;

    void Start()
    {
        //sets the deathScreen game object to false
        deathScreen.SetActive(false);
        //sets current health to 100
        currentHealth = maxHealth;
        //sets health on the health bar to 100 (function is in the HealthBar script)
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //if the player's health decreases to zero
        if (currentHealth <= 0){
            //makes the death screen visible
            deathScreen.SetActive(true);
            //makes the health bar invisible
            healthCanvas.SetActive(false);
            //sets the player game object to inactive
            player.SetActive(false);
            //sets the enemy game object to inactive
            enemy.SetActive(false);
        }

        if (player.transform.position.y <= -7)
        {
            currentHealth = 0;
        }
    }

    //a function that can be called so the player takes damage
    //this function is called in the Enemy script
    public void TakeDamage(int damage)
    {
        //subtracts damage from current health
        currentHealth -= damage;
        //sets the displayed health on the health bar to the new health value
        healthBar.SetHealth(currentHealth);
    }
}
