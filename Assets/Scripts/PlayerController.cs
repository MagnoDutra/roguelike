using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Gun")]
    [SerializeField] private Transform gunArm;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots;
    private float lastShot = 0;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

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
        }

        lastShot -= Time.deltaTime;
        lastShot = Mathf.Max(0, lastShot);
    }
}
