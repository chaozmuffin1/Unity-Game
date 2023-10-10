using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    public float moveSpeed=10;
    float jumpTime;
    bool jumping;
    bool jumpCancelled;
    public LayerMask jumpableGround;
    public Animator anim;
    public SpriteRenderer sprite;
    public bool runRight;
    public bool runLeft;
    public void Start(){
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.W))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed,rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);
        }
        
    }
    
    
    
    
    public void FixedUpdate(){
        if(jumpCancelled && jumping && rb.velocity.y > 0)
        {
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
        if(isGrounded() == false){
            anim.SetBool("jumping", true);
        }else{
            anim.SetBool("jumping", false);
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
    
}


