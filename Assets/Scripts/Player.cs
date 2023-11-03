using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public SpriteRenderer playerSpriteRenderer; // Reference to the SpriteRenderer Component
    [SerializeField] private GameObject deathScreen = null;

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
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
       
        if (currentHealth <= 0){
            deathScreen.SetActive(true);
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
