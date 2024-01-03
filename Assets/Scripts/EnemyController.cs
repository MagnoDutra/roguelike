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
    [SerializeField] private float shootRange;
    private float fireTimer;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {            
            AttackPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        anim.SetBool("isMoving", moveDir != Vector3.zero);
    }

    private void AttackPlayer()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDir = (PlayerController.instance.transform.position - transform.position).normalized;
        }
        else
        {
            moveDir = Vector2.zero;
        }

        rb.velocity = moveDir * moveSpeed;


        if (shouldShoot && fireTimer <= 0 && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
        {
            fireTimer = fireRate;
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(17);
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        AudioManager.instance.PlaySFX(2);
        Instantiate(hitEffect, transform.position, Quaternion.identity);

        if(health <= 0)
        {
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));

            AudioManager.instance.PlaySFX(1);
            Destroy(gameObject);
        }
    }
}
