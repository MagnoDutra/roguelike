using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] private GameObject[] brokenPieces;
    [SerializeField] private int maxPieces = 5;

    [SerializeField] private bool shouldDropItem;
    [SerializeField] private GameObject[] itemsToDrop;
    [SerializeField] private float itemDropPercent;

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

                AudioManager.instance.PlaySFX(0);
                
                // mostra os pedacos da caixa
                int piecesToDrop = Random.Range(0, maxPieces);

                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, brokenPieces.Length);

                    Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
                }

                //drop items
                if (shouldDropItem)
                {
                    float dropChance = Random.Range(0f, 100f);

                    if(dropChance < itemDropPercent)
                    {
                        int randomItem = Random.Range(0, itemsToDrop.Length);

                        Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                    }
                }
            }
        }
    }
}
