using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public Vector2 lookDirection;
    public float lookAngle;
    public GameObject bulletPrefab;
    private PlayerControls playerControls;
    //public float timeSinceLastShot;

    // Start is called before the first frame update
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        firePoint.rotation = Quaternion.Euler(0,0,0);
        lookDirection = Vector2.zero;
        lookDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition)) - transform.position);
        lookAngle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
        firePoint.rotation = Quaternion.Euler(0,0,lookAngle);
        /*if (transform.localEulerAngles.y != 180){
            lookAngle = Mathf.Clamp((Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg), -75, 75);
            firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        }else{
            if (transform.localEulerAngles.y != 0){
                lookDirection = Quaternion.AngleAxis(180, Vector3.up) * lookDirection;
                lookAngle = Mathf.Clamp((Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg), -75, 75);
                firePoint.rotation = Quaternion.Euler(0, -180, lookAngle);
            }
        }*/
//firepoint x pos was 1.2
        if (playerControls.Land.Fire.WasPressedThisFrame()){
            Shoot();
            //timeSinceLastShot = 0f;
        }else{
            //timeSinceLastShot += Time.deltaTime;
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    /*public void AmmoGaugeRecharge(){
        if (timeSinceLastShot >= 3f){
            //recharge smoothly
            //indicate in the game using lines on the slider for 1 shot, 2 shots, or a full bar
        }else{
            //don't recharge, call deplete method here
        }
    }

    public void AmmoGaugeDeplete(){
        //call in ammo recharge method, deplete slider by 33%
    }*/
}
