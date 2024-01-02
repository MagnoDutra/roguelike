using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector3 moveDir;

    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float lifeDuration = 3f;

    private SpriteRenderer sr;
    [SerializeField] private float fadeOutSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveDir.x = Random.Range(-moveSpeed, moveSpeed);
        moveDir.y = Random.Range(-moveSpeed, moveSpeed);

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * Time.deltaTime;

        moveDir = Vector3.Lerp(moveDir, Vector3.zero, deceleration * Time.deltaTime);

        lifeDuration -= Time.deltaTime;

        if(lifeDuration < 0)
        {
            sr.color = new Color(1, 1, 1, Mathf.MoveTowards(sr.color.a, 0, fadeOutSpeed * Time.deltaTime));

            if(sr.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
