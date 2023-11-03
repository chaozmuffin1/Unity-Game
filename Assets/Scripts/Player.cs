using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public SpriteRenderer playerSpriteRenderer; // Reference to the SpriteRenderer Component
    [SerializeField] private GameObject deathScreen = null;
    [SerializeField] private GameObject healthCanvas = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemy = null;

    
    /*[SerializeField] private bool active = true;
    private bool hasDied = false;*/

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
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
       
        if (currentHealth <= 0){
            deathScreen.SetActive(true);
            healthCanvas.SetActive(false);
            player.SetActive(false);
            enemy.SetActive(false);
        }
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
