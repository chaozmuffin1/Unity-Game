using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class ControlMenu : MonoBehaviour
{
    //[SerializeField] private InputActionReference jumpAction = null;
    //[SerializeField] private PlayerInput playerInput = null;
    //[SerializeField] private TMP_Text bindingDisplayText = null;
    //[SerializeField] private GameObject startRebindObject = null;
    //[SerializeField] private GameObject waitingForInputObject = null;

    //private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public string sceneName;

    /*public void startRebinding(){
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start();
    }*/

    /*private void RebindComplete(){
        rebindingOperation.Dispose();
        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }*/
    public void ReturnToMenu(){
        SceneManager.LoadScene(sceneName);
    }
}
