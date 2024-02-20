using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject playerObject;
    private int damageAmount = 20;
    public Player playerScript;
    public PlayerMovement playerMovementScript;
    public Animator anim;
    public float flightSpeed = 10;
    public Rigidbody2D rb;
    public bool val = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        anim = player.GetComponent<Animator>();
    }

    void Update(){
        if (val == false){
            DetectPlayer();
        }
    }

    private void MoveInDirection(){
        rb.velocity = new Vector2(-flightSpeed, rb.velocity.y);
    }

    private bool DetectPlayer(){
        val = false;
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < detectionRange){
            MoveInDirection();
            val = true;
        }
        return val;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == playerObject) // Check if the collision is made with player
        {
            anim.SetTrigger("hit"); // Play hit animation

            Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>(); // Apply force to player

            //Disable player's movement for a second
            PlayerMovement.instance.DisableMovementForCooldown();

            //Debug.Log("collision detected");
            playerScript.TakeDamage(damageAmount);

            Destroy(knifeObject);
        }
    }
}
