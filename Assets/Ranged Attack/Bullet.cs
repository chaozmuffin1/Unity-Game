using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 5;
    public Rigidbody2D rb;

    public Vector3 oldPos;
    public float totalDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        oldPos = transform.position;
    }

    void Update(){
        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        totalDistance += distanceThisFrame;
        oldPos = transform.position;
        if (totalDistance >= 20){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        TilemapCollider2D tileCollider = hitInfo.GetComponent<TilemapCollider2D>();
        if (enemy != null){
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (tileCollider != null){
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
