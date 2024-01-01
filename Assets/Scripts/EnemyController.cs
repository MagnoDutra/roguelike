using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeToChasePlayer;
    [SerializeField] public int health = 150;
    [SerializeField] public GameObject[] deathSplatters;
    [SerializeField] public GameObject hitEffect;

    private Vector3 moveDir;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private bool shouldShoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDir = (PlayerController.instance.transform.position - transform.position).normalized;
        }
        else
        {
            moveDir = Vector3.zero;
        }

        rb.velocity = moveDir * moveSpeed;

        anim.SetBool("isMoving", moveDir != Vector3.zero);

        if(shouldShoot && fireTimer <= 0)
        {
            fireTimer = fireRate;
            Instantiate(bullet, firePoint.position, Quaternion.identity);
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        if(health <= 0)
        {
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));

            Destroy(gameObject);
        }
    }
}
