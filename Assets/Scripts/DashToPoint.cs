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
    public bool moving;
    public bool closeToPos;
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
        //add code so player can't dash to a point behind them if they are moving the opposite direction
        if (Input.GetKeyDown("c")/*Input.GetMouseButtonDown(1)*/ && moving == false){
            GetInitialPosition();
            playerMovementScript.isMovementEnabledDash = false;
            rb.velocity = Vector2.zero;
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //lastClickedPos.z = 0;
            //relLastClickedPos = transform.position - lastClickedPos;/*transform.InverseTransformPoint(lastClickedPos);*/
            //relLastClickedPos.z = 0;
            //Debug.Log("relLastClickedPos:" + relLastClickedPos);
            //float angle = Vector3.Angle(Vector3.right, relLastClickedPos);
            //float angle = Mathf.Atan2(relLastClickedPos.x, relLastClickedPos.y) * Mathf.Rad2Deg;
            //Debug.Log("angle: " + angle);
            //Vector3 relativeTargetPos = new Vector3(maxDistance*Mathf.Cos(angle), maxDistance*Mathf.Sin(angle), 0);
            //targetPos = relativeTargetPos + transform.position; /*+ transform.position;*/
            //Debug.Log("player position: " + transform.position);
            //Debug.Log("lastClickedPos: " + lastClickedPos);
            //Debug.Log("targetPos: " + targetPos);
            targetPos = lastClickedPos;
            moving = true;
        }
        //Vector2 difference = lastClickedPos - (Vector2)transform.position;
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


    //Vector2 difference = lastClickedPos - (Vector2)transform.position;

    //rb.velocity.x>0 && difference.x > 0 
}
