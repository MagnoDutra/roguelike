using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth { get; private set; }
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibleLength;
    private float invincibleTimer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;

            if(invincibleTimer <= 0)
            {
                PlayerController.instance.bodySr.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void DamagePlayer()
    {
        if(invincibleTimer <= 0)
        {
            currentHealth--;
            invincibleTimer = invincibleLength;
            PlayerController.instance.bodySr.color = new Color(1,1,1,0.5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }

    }

    public void MakeInvincible(float time)
    {
        invincibleTimer = time;
        PlayerController.instance.bodySr.color = new Color(1, 1, 1, 1);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
