using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToPoint : MonoBehaviour
{
    public Animator animator;
    //gets the player's rigid body
    public Rigidbody2D rb;
    //a multiplier for the dash speed used in MoveTowards method
    public float dashSpeed = 2000;
    //the target position the player will dash to
    Vector2 targetPos;
    //boolean used to restrict changing targetPos when the player is already moving
    public bool currentlyDashing = false;
    public bool oldCurrentlyDashing = false;
    //boolean used to determine if the player is close to the target position or not
    public bool closeToPos = false;
    //the maximum distance (tangential) that the player can dash
    public float maxDistance = 5;
    //used to determine current difference between the player's position before the dash and their current position while dashing each frame
    public Vector2 position;
    //reference to the player movement script
    public PlayerMovement playerMovementScript;
    //reference to the player's input component
    //private PlayerControls playerControls;
    public ParticleSystem trail;

    public bool canDash = true;

    public void Start(){
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        var emission = trail.emission;
        DetectChangeInCurrentlyDashing();
        //if the dash key is pressed and currentlyDashing is false (meaning the player isn't currently dashing)
        if (Input.GetKeyDown("c") && currentlyDashing == false && canDash == true){
            animator.SetBool("dashing", true);
            //gets the player's current position before dashing
            GetInitialPosition();
            //disables all other movement in the player movement component of the player besides the dash
            playerMovementScript.isMovementEnabledDash = false;
            //sets the player's current velocity to zero (to avoid weird physics glitches)
            rb.velocity = Vector2.zero;
            //gets the target position, which is the mouse position on the screen when the dash key is pressed
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //sets currentlyDashing to true
            currentlyDashing = true;
            canDash = false;
            }
        //if player is currently dashing, in other words if there is a target position, and if the player isn't grounded, then the dash function continues
        //player only continues dashing if the distance between targetPos and the player's initial position is greater than 2 and less than 7.5 (used for closeToPos)
        if (currentlyDashing && (targetPos - (Vector2)transform.position).magnitude >= 2 && ((Vector2)transform.position - position).magnitude <= 7.5f && playerMovementScript.isGrounded() == false){
            emission.enabled = true;
            //sets the current dash cooldown to zero in player movement
            GetComponent<PlayerMovement>().currentDashCooldown = 0;
            //gets rid of gravity temporarily
            GetComponent<Rigidbody2D>().gravityScale = 0;
            //the distance that will be moved each frame
            float step =  0.017f + (dashSpeed * Time.deltaTime);
            //move the player towards the target position at a speed of 'step'
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            //player isn't close to target pos
            closeToPos = false;
        }else{
            emission.enabled = false;
            animator.SetBool("dashing", false);
            //player is no longer dashing
            oldCurrentlyDashing = currentlyDashing;
            currentlyDashing = false;
            DetectChangeInCurrentlyDashing();
            //gravity is back
            GetComponent<Rigidbody2D>().gravityScale = 2;
            //the player is close to the targetPosition
            closeToPos = true;
            //trail.Stop();
        }
    }
    public void DetectChangeInCurrentlyDashing(){
        if (oldCurrentlyDashing != currentlyDashing && currentlyDashing == false){
            //player has completed a dash and cannot dash again until they reach the ground
            TurnOff();
            canDash = false;
        }else{
            canDash = true;
        }
    }
    public void TurnOn(){
        //turns on this script
        this.enabled = true;
    }
    public void OnEnable(){
        //dashTrail.Play();
        canDash = true;
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        closeToPos = false;
    }
    public void GetInitialPosition(){
        //gets the player's position before they start the dash
        position = transform.position;
    }
    public void TurnOff(){
        //only executes if player is close to target position
        if (closeToPos == true){
        //gravity is back
        GetComponent<Rigidbody2D>().gravityScale = 2;
        //player isn't currently dashing
        currentlyDashing = false;
        //resets target position to zero
        targetPos = Vector2.zero;
        //enables movement again
        playerMovementScript.EnableMovementAfterDash();
        //animator.SetBool("dashing", false);
        //turns off this script
        this.enabled = false;
        }
    }
}
