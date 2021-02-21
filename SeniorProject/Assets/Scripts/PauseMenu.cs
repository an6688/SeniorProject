using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject resumeButton;

    private int sceneToContinue;

    public string mainMenuScene;

    public GameObject pauseMenu; 

    
    public bool isPaused; 


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Game_Scene"))
        {
            resumeButton.SetActive(true);
        }
        else
        {
            resumeButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true; 
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void Continue()
    {
        sceneToContinue = PlayerPrefs.GetInt("Game_Scene");
        if (sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            return;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
