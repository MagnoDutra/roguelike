using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    [SerializeField] private float waitToBeCollected = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && waitToBeCollected <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            AudioManager.instance.PlaySFX(7);
            Destroy(gameObject);
        }
    }
}
