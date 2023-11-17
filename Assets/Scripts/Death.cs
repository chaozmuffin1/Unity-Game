using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    //gets the name of a scene
    public string sceneName;
    
    public void ReturnToMenu(){
        //returns to the scene with a name that is set in the inspector menu
        SceneManager.LoadScene(sceneName);
    }
}
