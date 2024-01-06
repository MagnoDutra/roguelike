using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer bodySr;

    [Header("Gun")]
    [SerializeField] private Transform gunArm;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots;
    private float lastShot = 0;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed = 8;
    [SerializeField] private float dashLength = 0.5f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashInvincibility = .5f;

    public float dashTimer { get; private set; }
    private float dashCDTimer;
    private float activeMoveSpeed;
    private Vector2 moveInput;


    public bool canMove { get; set; } = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) 
        {   
            rb.velocity = Vector3.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        if (moveInput != Vector2.zero) 
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        
        Vector3 mousePos = Input.mousePosition; // Posicao do mouse em PIXELS
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition); // Posicao do player transformado em pixels

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gunArm.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            gunArm.localScale = new Vector3(1, 1, 1);
        }

        // Rotate gun
        Vector2 mouseDistance = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg; // Atan retorna em radianos, Multiplico pela constante que me devolvera em graus

        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        if(Input.GetMouseButton(0) && lastShot <= 0)
        {
            Instantiate(shootPrefab, firePoint.position, firePoint.rotation);
            lastShot = timeBetweenShots;
            AudioManager.instance.PlaySFX(12);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCDTimer <= 0 && dashTimer <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashTimer = dashLength;

                anim.SetTrigger("dash");
                AudioManager.instance.PlaySFX(8);
                PlayerHealthController.instance.MakeInvincible(dashInvincibility);
            }
        }

        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCDTimer = dashCooldown;
            }
        }

        if(dashCDTimer > 0)
        {
            dashCDTimer -= Time.deltaTime;
        }

        lastShot -= Time.deltaTime;
        lastShot = Mathf.Max(0, lastShot);
    }
}
