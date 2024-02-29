using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WpnCrshrEnableDisable : MonoBehaviour
{
    public Weapon weaponScript;
    public Crosshair crosshairScript;
    public PlayerCombat meleeScript;
    bool enOrDis;
    private PlayerControls playerControls;

    public Image ammoCircle;
    public Image ammoBackground;
    public TMP_Text ammoCount;

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
        meleeScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        meleeScript.enabled = true;
        weaponScript.enabled = false;
        crosshairScript.enabled = false;
        ammoCircle.enabled = false;
        ammoCount.enabled = false;
        ammoBackground.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Land.EnableFire.WasPressedThisFrame() && enOrDis == false){
            weaponScript.enabled = true;
            crosshairScript.enabled = true;
            crosshairScript.OnEnable();
            ammoCircle.enabled = true;
            ammoCount.enabled = true;
            ammoBackground.enabled = true;
            enOrDis = true;
            meleeScript.enabled = false;
        } 
        if (playerControls.Land.EnableMelee.WasPressedThisFrame() && enOrDis == true){
            weaponScript.enabled = false;
            crosshairScript.OnDisable();
            meleeScript.enabled = true;
            crosshairScript.enabled = false;
            ammoCircle.enabled = false;
            ammoBackground.enabled = false;
            ammoCount.enabled = false;
            enOrDis = false;
        }
    }

    public void Disable(){
        weaponScript.enabled = false;
        crosshairScript.OnDisable();
        crosshairScript.enabled = false;
        ammoCircle.enabled = false;
        ammoBackground.enabled = false;
        ammoCount.enabled = false;
    }
}
