using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    //variable that is later set to sprite's rigid body
    public Rigidbody2D rb;
    //variable that is later set to the sprite's collider
    public BoxCollider2D coll;
    //the maximum amount of time that will be read as a jump input
    public float buttonTime = 0.5f;
    //the vertical distance the player is able to jump
    public float jumpHeight = 5;
    //adds initial downwards force after a jump is cancelled
    public float cancelRate = 100;
    //the player's movement speed
    public float moveSpeed = 8;
    public DashToPoint dashScript;
    public bool dashReady = false;
    public float dashCooldown = 10f;
    public float currentDashCooldown = 0f;
    //is set to the amount of time that the player holds on to the jump key, for variable jumping
    float jumpTime;
    //is true when the player hits the jump key and is currently grounded
    bool jumping;
    //set to true when player releases the jump key and false otherwise
    bool jumpCancelled;
    //A layer mask so a later raycast will only detect collisions with the layer 'jumpable ground'
    public LayerMask jumpableGround;
    //animator component that is later set to the sprite's animator
    public Animator anim;
    public SpriteRenderer sprite;
    //checks if the player is running to the right
    public bool runRight;
    //checks if the player is running to the left
    public bool runLeft;
    //checks if player can move or if the player is on knockback cooldown
    private bool isMovementEnabled = true;
    //amount of seconds player is on cooldown for
    private float movementCooldown = 1f;
    //timer that will count until it equals the movement cooldown
    private float movementCooldownTimer = 0f;
    //reference to the player's input component
    private PlayerControls playerControls;
    //physics material that has no friction
    [SerializeField] private PhysicsMaterial2D material;
    //physics material that has friction
    [SerializeField] private PhysicsMaterial2D defaultMaterial;

    public static PlayerMovement instance;

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
    public void Start(){
        //gets the player's box collider
        coll = GetComponent<BoxCollider2D>();
        //gets the player's animation component
        anim = GetComponent<Animator>();
        //gets the player's sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        instance = this;
        //sets attack to false on the first frame
        anim.SetBool("attack",false);
        dashScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DashToPoint>();
    }
    public void Update(){
        
        if (isGrounded() == false && dashReady == true){
            dashScript.TurnOn();
        }else{
            dashScript.TurnOff();
        }
        if (currentDashCooldown >= dashCooldown){
            dashReady = true;
        }else{
            dashReady = false;
            currentDashCooldown += Time.deltaTime;
            currentDashCooldown = Mathf.Clamp(currentDashCooldown, 0.0f, dashCooldown);
        }
        if (isMovementEnabled){
        
        //layermask component for the boxcast so it only detects collisions with ground
        LayerMask mask = LayerMask.GetMask("Ground");

        //A boxcast of the sprite's collider to the right that detects if the player is touching a vertical wall
        //this boxcasts also unintentionally detects if the player is touching ground from below it, but this is not an issue at the moment
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, coll.bounds.size, 0f, Vector3.right, 0.1f, mask);

        //if hit is true, meaning a collision between the casted box and the wall is detected, the material of the player's rigid body is changed to have zero friction
        //this is to prevent the player to be able to stick to walls by moving to the right while touching a wall
        //existing friction between the player and the ground is responsible for making the player stick to a wall
        if (hit){
            rb.sharedMaterial = material;
        }
        else{
        //if a collision is not detected the player is on the ground or in the air and the player will have friction
            rb.sharedMaterial = defaultMaterial;
        }
        
        if(playerControls.Land.Jump.triggered && isGrounded()){
            rb.gravityScale = 2;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        bool isJumpHeld = playerControls.Land.Jump.ReadValue<float>() > 0.1f;
        if (jumping){
            jumpTime += Time.deltaTime;
            if (isJumpHeld == false)
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
        if (playerControls.Land.Right.IsPressed()){
            rb.velocity = new Vector2(moveSpeed,rb.velocity.y);
        }
        if (playerControls.Land.Left.IsPressed()){
            rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);
        }
        }else{
            movementCooldownTimer -= Time.deltaTime;
            if (movementCooldownTimer <= 0f)
            {
                isMovementEnabled = true;
            }
        }
    }
    
    public void FixedUpdate(){
        if(jumpCancelled && jumping && rb.velocity.y > 0){
            rb.AddForce(Vector2.down * cancelRate);
        }
        if(rb.velocity.x<-1 && isGrounded()){
            anim.SetBool("running",true);
            sprite.flipX = true;
            runLeft = true;
            runRight = false;
        }else{
            if(rb.velocity.x>1 && isGrounded()){
            anim.SetBool("running",true);
            sprite.flipX = false;
            runRight = true;
            runLeft = false;
        }else{
            runLeft = false;
            runRight = false;
        }
        }
        if(isGrounded() == false && rb.velocity.y>0){
            anim.SetBool("jumping", true);
        }else{
            if(isGrounded() == false && rb.velocity.y<=0){
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }else{
                anim.SetBool("jumping", false);
                anim.SetBool("falling", false);
            }
            
        }
        if (runRight == false && runLeft == false){
            anim.SetBool("running",false);
        }else{
            anim.SetBool("running", true);
        }
    }
    
    public bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public void DisableMovementForCooldown()
    {
        isMovementEnabled = false;
        movementCooldownTimer = movementCooldown;
    }
}
    



