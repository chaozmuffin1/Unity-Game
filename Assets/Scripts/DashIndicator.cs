using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIndicator : MonoBehaviour
{
    public DashToPoint dashScript;
    public PlayerMovement playerMovementScript;
    public GameObject dashIndicator;
    void Start(){
        dashScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DashToPoint>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    void Update(){

    }

    /*void TurnOff(){
        dashIndicator.SetActive(false);
    }
    void TurnOn(){
        dashIndicator.SetActive(true);
        //dashIndicator.transform.position
    }*/
    /*//public Texture2D cursorArrow;
    public DashToPoint dashScript;
    public PlayerMovement playerMovementScript;
    public float crosshairSpeed = 1000f;
    public float radius = 7.5f;
    //public Texture2D cursorArrowUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        //Cursor.visible = false;
        dashScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DashToPoint>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        transform.position = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible = false;
        if (Input.GetMouseButtonDown(0)){
            DisableMovementForIndicator();
        }else{
            
            var input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            var vectorFromParent = transform.localPosition + new Vector3(input.x, input.y, 0) * crosshairSpeed * Time.deltaTime;
            transform.localPosition = Vector2.ClampMagnitude(vectorFromParent, radius);
        }
    }
    public IEnumerator DisableMovementForIndicator(){
        yield return new WaitForSeconds(3.0f);
    }*/
}
