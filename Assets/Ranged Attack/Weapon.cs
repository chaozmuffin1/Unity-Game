using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{

    public Image ammoCircle;

    public Transform firePoint;
    public Vector2 lookDirection;
    public float lookAngle;
    public GameObject bulletPrefab;
    private PlayerControls playerControls;

    [SerializeField] private int maxAmmo = 3;
    private int currentAmmo;
    private float fraction;

    public TMP_Text ammoCount;

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
        currentAmmo = maxAmmo;
        fraction = (1f/(float)maxAmmo); 
    }

    // Update is called once per frame
    void Update()
    {
        firePoint.rotation = Quaternion.Euler(0,0,0);
        lookDirection = Vector2.zero;
        lookDirection = ((Camera.main.ScreenToWorldPoint(Input.mousePosition)) - transform.position);
        lookAngle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
        firePoint.rotation = Quaternion.Euler(0,0,lookAngle);
        
        if (playerControls.Land.Fire.WasPressedThisFrame() && currentAmmo > 0){
            Shoot();
            currentAmmo -= 1;
            ammoCircle.fillAmount = ammoCircle.fillAmount - fraction;
        }
        //changes the UI text for the ammo count
        ammoCount.SetText(currentAmmo.ToString());
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Reload(){
        //reloading occurs when an enemy is defeated, and the amount reloaded will eventually depend on the type of enemy
        if (currentAmmo < maxAmmo){
            currentAmmo += 1;
            ammoCircle.fillAmount = ammoCircle.fillAmount + fraction;
        }
    }
}
