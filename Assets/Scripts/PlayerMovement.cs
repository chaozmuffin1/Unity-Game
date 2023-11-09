using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    public float moveSpeed=8;
    float jumpTime;
    bool jumping;
    bool jumpCancelled;
    public LayerMask jumpableGround;
    public Animator anim;
    public SpriteRenderer sprite;
    public bool runRight;
    public bool runLeft;
    private bool isMovementEnabled = true;
    private float movementCooldown = 1f;
    private float movementCooldownTimer = 0f;
    public static PlayerMovement instance;
    private PlayerControls playerControls;
    [SerializeField] private PhysicsMaterial2D material;
    [SerializeField] private PhysicsMaterial2D defaultMaterial;

    public void Awake(){
        playerControls = new PlayerControls();
    }
    public void OnEnable(){
        playerControls.Enable();
    }
    public void OnDisable(){
        playerControls.Disable();
    }
    public void Start(){
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        instance = this;
        anim.SetBool("attack",false);
    }
    public void Update(){
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
        
        if(/*Input.GetKeyDown(KeyCode.W)*/ playerControls.Land.Jump.triggered && isGrounded()){
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        bool isJumpHeld = playerControls.Land.Jump.ReadValue<float>() > 0.1f;
        if (jumping){
            jumpTime += Time.deltaTime;
            if (isJumpHeld == false /*Input.GetKeyUp(KeyCode.W)*/)
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
        if (/*Input.GetKey(KeyCode.D)*/playerControls.Land.Right.IsPressed()){
            rb.velocity = new Vector2(moveSpeed,rb.velocity.y);
        }
        if (/*Input.GetKey(KeyCode.A)*/playerControls.Land.Left.IsPressed()){
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
    



