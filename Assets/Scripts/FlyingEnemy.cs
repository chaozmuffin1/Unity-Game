using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] float radius;
    private Vector2 fireDirection;
    private float fireAngle;
    private bool val = false;
    [SerializeField] private bool isTurret;
    [SerializeField] private float firesPerSecond;
    private float currentCooldown;
    [SerializeField] private float verticalPatrolDistance;
    private Vector2 patrolPointOne;
    private Vector2 patrolPointTwo;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    [SerializeField] private float patrolSpeed;
    private Vector3 baseScale;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private GameObject colliders;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        patrolPointOne = (Vector2.up * verticalPatrolDistance) + (Vector2)transform.position;
        patrolPointTwo = (Vector2.up * -verticalPatrolDistance) + (Vector2)transform.position;
        targetPos = patrolPointOne;
        rb.velocity = new Vector2(0, patrolSpeed);
    }

    void Update(){
        if (isTurret == true){
            TurretBehavior();
            if (DetectPlayer() == true && currentCooldown >= firesPerSecond){
                shootSound.time = 0.01f;
                shootSound.Play();
                Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
                currentCooldown = 0.0f;
            }else{
                currentCooldown += Time.deltaTime;
                currentCooldown = Mathf.Clamp(currentCooldown, 0.0f, firesPerSecond);
            }
        }
    }

    private bool DetectPlayer(){
        val = false;
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < radius){
            val = true;
        }
        return val;
    }

    private void TurretBehavior(){
        fireDirection = player.position - transform.position;
        fireAngle = (Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
        firePoint.rotation = Quaternion.Euler(0,0,fireAngle);

        if (transform.position.y >= patrolPointOne.y && targetPos.y == patrolPointOne.y){
            rb.velocity = new Vector2(0, -patrolSpeed);
            targetPos = patrolPointTwo;
        }else{
            if (transform.position.y <= patrolPointTwo.y && targetPos.y == patrolPointTwo.y){
                rb.velocity = new Vector2(0, patrolSpeed);
                targetPos = patrolPointOne;
            }else{
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        Vector3 newScale = baseScale;
        if (player.position.x <= transform.position.x){
            newScale.x = baseScale.x;
        }else{
            newScale.x = -baseScale.x;
        }
        colliders.transform.localScale = newScale;
    }
}