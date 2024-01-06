using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float waitToLoad = 4f;
    [SerializeField] private string nextLevel;
    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();

        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextLevel); 
    }

    public void PauseUnpause() 
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
