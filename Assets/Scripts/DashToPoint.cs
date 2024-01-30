using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToPoint : MonoBehaviour
{
//the player's dash speed
    public Rigidbody2D rb;
    public float dashSpeed = 2000;
    Vector2 lastClickedPos;
    Vector3 relLastClickedPos;
    public bool moving = false;
    public bool closeToPos = false;
    public float maxDistance = 5;
    public Vector2 position;
    public Vector3 targetPos;
    public PlayerMovement playerMovementScript;

    public void Start(){
        TurnOff();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown("c") && moving == false){
            GetInitialPosition();
            playerMovementScript.isMovementEnabledDash = false;
            rb.velocity = Vector2.zero;
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = lastClickedPos;
            moving = true;
        }
        if (moving && (lastClickedPos - (Vector2)transform.position).magnitude >= 2 && ((Vector2)transform.position - position).magnitude <= 7.5f && playerMovementScript.isGrounded() == false /*&& ((rb.velocity.x>0 && (lastClickedPos - (Vector2)transform.position).x > 0) || (rb.velocity.x<0 && (lastClickedPos - (Vector2)transform.position).x < 0) || rb.velocity.x = 0)*/){
            playerMovementScript.isMovementEnabledDash = false;
            GetComponent<PlayerMovement>().currentDashCooldown = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            float step =  0.017f + (dashSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            closeToPos = false;
        }else{
            moving = false;
            GetComponent<Rigidbody2D>().gravityScale = 2;
            closeToPos = true;
        }
        //ensure moving = false, lastClickedPos != null, closeToPos = true, 
    }
    public void TurnOn(){
        this.enabled = true;
    }
    public void GetInitialPosition(){
        position = transform.position;
    }
    public void TurnOff(){
        if (closeToPos == true){
        GetComponent<Rigidbody2D>().gravityScale = 2;
        moving = false;
        lastClickedPos = Vector2.zero;
        playerMovementScript.EnableMovementAfterDash();
        this.enabled = false;
        }
    }
}
