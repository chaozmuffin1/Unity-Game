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
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject colliders;
    

    void Start(){
        baseScale = colliders.transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    private void FixedUpdate(){
        if (colliders != null){
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
    }

    private void ChasePlayer(){
        if (colliders != null){
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
    }

    private void ChangeFacingDirection(string newDirection){
        Vector3 newScale = baseScale;
        if (newDirection == LEFT){
            newScale.x = -baseScale.x;
        }else{
            newScale.x = baseScale.x;
        }
        //canvas.transform.SetParent(null, false);
        if (colliders != null){
            colliders.transform.localScale = newScale;
        }
        //canvas.transform.SetParent(transform, false);
        facingDirection = newDirection;
    }

    private bool IsHittingWall(){
        bool val = false;
        float castDist = baseCastDistance;

        if (facingDirection == LEFT){
            castDist = -baseCastDistance;
        }

        if (colliders != null){
            Vector3 targetPos = castPos.position;
            targetPos.x += castDist;

            Debug.DrawLine(castPos.position, targetPos, Color.blue);
        
            if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))){
                val = true;
            }else{
                val = false;
            }
        }
        return val;
    }

    private bool IsNearEdge(){
        bool val = true;
        float castDist = baseCastDistance;

        if (colliders != null){
            Vector3 targetPos = castPos.position;
            targetPos.y -= castDist;

            Debug.DrawLine(castPos.position, targetPos, Color.red);
        
            if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))){
                val = false;
            }else{
                val = true;
            }
        }

        return val;
    }
}