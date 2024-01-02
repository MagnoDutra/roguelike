using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] private GameObject[] brokenPieces;
    [SerializeField] private int maxPieces = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(PlayerController.instance.dashTimer > 0)
            {
                Destroy(gameObject);
                
                int piecesToDrop = Random.Range(0, maxPieces);

                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, brokenPieces.Length);

                    Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
                }
            }
        }
    }
}
