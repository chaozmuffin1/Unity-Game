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
    [SerializeField] private GameObject healthCanvas = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemy = null;

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
}
