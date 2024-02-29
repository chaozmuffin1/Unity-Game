using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondKnifeHitbox : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Behaviour playerHitbox;

    void OnCollisionEnter2D(Collision2D collision)
    {
        playerHitbox.enabled = false;
        if (collision.gameObject != playerObject)
        {
            playerHitbox.enabled = false;
        }else{
            
        }
    }
}
