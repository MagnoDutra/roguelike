using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public GameObject deathScreen;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    private bool fadeToBlack;
    private bool fadeOutBlack;

    [SerializeField] private string newGameScene;
    [SerializeField] private string mainMenuScene;
    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{healthSlider.value} / {healthSlider.maxValue}";

        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(0, 0, 0, Mathf.MoveTowards(fadeScreen.color.a, 0, fadeSpeed * Time.deltaTime));

            fadeOutBlack = fadeScreen.color.a != 0f;
        }

        if(fadeToBlack)
        {
            fadeScreen.color = new Color(0, 0, 0, Mathf.MoveTowards(fadeScreen.color.a, 1, fadeSpeed * Time.deltaTime));

            fadeToBlack = fadeScreen.color.a != 1f;
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
