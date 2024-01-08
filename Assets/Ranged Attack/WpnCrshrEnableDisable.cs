using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpnCrshrEnableDisable : MonoBehaviour
{
    public Weapon weaponScript;
    public Crosshair crosshairScript;
    bool enOrDis;
    private PlayerControls playerControls;

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
    // Start is called before the first frame update
    void Start()
    {
        enOrDis = false;
        weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        crosshairScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Crosshair>();
        weaponScript.enabled = false;
        crosshairScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Land.EnableFire.WasPressedThisFrame() && enOrDis == false){
            weaponScript.enabled = true;
            crosshairScript.enabled = true;
            crosshairScript.OnEnable();
        } 
    }

    public void Disable(){
        weaponScript.enabled = false;
        crosshairScript.OnDisable();
        crosshairScript.enabled = false;
    }
}
