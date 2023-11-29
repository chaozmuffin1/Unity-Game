using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToPoint : MonoBehaviour
{
//the player's dash speed
    public Rigidbody2D rb;
    public float dashSpeed = 14;
    Vector2 lastClickedPos;
    public bool moving;
    public bool closeToPos;
    

    public void Start(){
        TurnOff();
    }
    void Update()
    { 
        if (Input.GetMouseButtonDown(0)){
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        if (moving && ((Vector2)transform.position - lastClickedPos).magnitude >= 2){
            GetComponent<PlayerMovement>().currentDashCooldown = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            float step = dashSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
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
    public void TurnOff(){
        if (closeToPos == true){
        GetComponent<Rigidbody2D>().gravityScale = 2;
        moving = false;
        lastClickedPos = Vector2.zero;
        this.enabled = false;
        }
    }
}
