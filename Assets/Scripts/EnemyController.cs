using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangeToChasePlayer;
    [SerializeField] public int health = 150;

    private Vector3 moveDir;
    private Rigidbody2D rb;
    private Animator anim;

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
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}