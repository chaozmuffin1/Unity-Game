using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public float attackRange = 1.05f;
    public LayerMask enemyLayers;
    public float knockbackForce = 5f;
    public Rigidbody2D rb;
    public bool runRight = true;

    //checks if this is the player's first attack since starting the game
    public bool firstAttack = true;
    //boolean to determine if the player can attack or not
    public bool attackReady;
    //time player has to wait between attacks
    public float cooldown = 1.5f;
    //the time that has passed since the player last attacked
    public float cooldownCurrent = 0.0f;
    [SerializeField] private AudioSource attackAudio;

    private PlayerControls playerControls;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    public void Awake(){
        playerControls = new PlayerControls();
    }
    //enables the player inputs
    public void OnEnable(){
        playerControls.Enable();
    }
    //disables the player inputs
    public void OnDisable(){
        playerControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0){
            runRight = true;
        }else{
            runRight = false;
        }
        if (cooldownCurrent >= cooldown){
            //if the time since the last attack is greater than the cooldown the player can attack
            //will not update cooldownCurrent 
            attackReady = true;
        }else{
            //if the time since the last attack is less than the cooldown the player needs to wait
            attackReady = false;
            //adds elapsed time since starting the game to the time that has passed since the last attack
            cooldownCurrent += Time.deltaTime;
            //limits the range of cooldownCurrent to be between 0 and the cooldown
            cooldownCurrent = Mathf.Clamp(cooldownCurrent, 0.0f, cooldown);
        }
        //function that is used for every attack after the first attack
        if (playerControls.Land.MeleeAttack.WasPressedThisFrame() && attackReady && firstAttack == false)
        {
            attackAudio.Play();
            Attack();
            //resets the time since the last attack to 0 so the player needs to wait again
            cooldownCurrent = 0.0f;
        }
        //seperate function for the first attack so the player doesn't have to wait cooldownCurrent to be greater than cooldown
        if (playerControls.Land.MeleeAttack.WasPressedThisFrame() && firstAttack){
            attackAudio.Play();
            Attack();
            //sets firstAttack to false so this function isn't called again
            firstAttack = false;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies;
        // Play an attack animation
        animator.SetTrigger("attack");

        // Detect enemies in range of attack
        hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);

        // continue video at 7:32
        // Damage enemies in range of attack
        foreach(Collider2D enemy in hitEnemies)
        {
            // Get the enemy's rigidbody
            //Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

            // Calculate the knockback direction (from enemy to player)
            //Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;

            // Apply knockback force to the enemy
            //enemyRigidbody.velocity = knockbackDirection * knockbackForce;

            // Damage the enemy
            enemy.GetComponent<EnemyHealth>().TakeDamage(5);

            // Disable the enemy's movement for a second
            //enemy.GetComponent<Enemy>().DisableMovementForCooldown();
        }
    }

    /*void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/
}
