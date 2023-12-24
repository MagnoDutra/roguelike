using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Transform gunArm;

    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        rb.velocity = moveInput * moveSpeed;

        Vector3 mousePos = Input.mousePosition; // Posicao do mouse em PIXELS
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition); // Posicao do player transformado em pixels

        Vector2 mouseDistance = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg; // Atan retorna em radianos, Multiplico pela constante que me devolvera em graus

        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}
