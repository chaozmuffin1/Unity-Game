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
        if (transform.localEulerAngles.y != 180){
            lookAngle = Mathf.Clamp((Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg), -75, 75);
            firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        }else{
            if (transform.localEulerAngles.y != 0){
                lookDirection = Quaternion.AngleAxis(180, Vector3.up) * lookDirection;
                lookAngle = Mathf.Clamp((Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg), -75, 75);
                firePoint.rotation = Quaternion.Euler(0, -180, lookAngle);
            }
        }
        //lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        //Debug.Log(lookAngle);
        //firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (playerControls.Land.Fire.WasPressedThisFrame()){
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
