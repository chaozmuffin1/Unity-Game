using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] private Transform castPos;
    [SerializeField] private float baseCastDistance;
    private const string LEFT = "left";
    private const string RIGHT = "right";
    private string facingDirection;
    private Vector3 baseScale;
    private Rigidbody2D rb;
    public float patrolSpeed = 5;
    public float agroSpeed = 8;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float agroRange;
    public Animator anim;
    private int currentHealth;
    private int maxHealth = 20;
    public FloatingHealthBar healthBar;
    public Player playerScript;
    public PlayerMovement playerMovementScript;
    private int damageAmount = 20;
    [SerializeField] private GameObject canvas;
    public Transform parent;
    [SerializeField] private Transform healthBarObject;
    [SerializeField] private Transform background;
    [SerializeField] private Transform fillArea;
    [SerializeField] private Transform fill;
    

    void Start(){
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    private void FixedUpdate(){
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange){
            //chase player
            ChasePlayer();
        }else{
            //patrol
            float vX = patrolSpeed;
            if (facingDirection == LEFT){
            vX = -patrolSpeed;
            }
            rb.velocity = new Vector2(vX, rb.velocity.y);
            if(IsHittingWall() || IsNearEdge()){
                if (facingDirection == LEFT){
                ChangeFacingDirection(RIGHT);
                }else{
                    ChangeFacingDirection(LEFT);
                }
            }
        }
    }

    private void ChasePlayer(){
        if (transform.position.x < player.position.x){
            rb.velocity = new Vector2(agroSpeed, rb.velocity.y);
            if (facingDirection == LEFT){
                ChangeFacingDirection(RIGHT);
            }
        }else{
            rb.velocity = new Vector2(-agroSpeed, rb.velocity.y);
            if (facingDirection == RIGHT){
                ChangeFacingDirection(LEFT);
            }
        }
    }

    private void ChangeFacingDirection(string newDirection){
        Vector3 newScale = baseScale;
        if (newDirection == LEFT){
            newScale.x = -baseScale.x;
        }else{
            newScale.x = baseScale.x;
        }
        //canvas.transform.SetParent(null, false);
        transform.localScale = newScale;
        //canvas.transform.SetParent(transform, false);
        facingDirection = newDirection;
    }

    private bool IsHittingWall(){
        bool val = false;
        float castDist = baseCastDistance;

        if (facingDirection == LEFT){
            castDist = -baseCastDistance;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))){
            val = true;
        }else{
            val = false;
        }

        return val;
    }

    private bool IsNearEdge(){
        bool val = true;
        float castDist = baseCastDistance;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);
        
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))){
            val = false;
        }else{
            val = true;
        }

        return val;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("damage");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
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