using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public string sceneName;
    public void ReturnToMenu(){
        SceneManager.LoadScene(sceneName);
    }
}
