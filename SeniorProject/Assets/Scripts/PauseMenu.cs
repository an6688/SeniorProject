using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml; 
using System.Xml.Serialization;

public class PauseMenu : MonoBehaviour
{
    public GameObject resumeButton;

    public GameObject pauseMenu;

    private GameManager _gameManager; 

    private int sceneToContinue;

    public string mainMenuScene;

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

    public void Awake()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        Load();
    }

    public void Save()
    {
        Debug.Log("saving!!");
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(file, _gameManager.collectedCandy);
        }
        catch (SerializationException e)
        {
            Debug.LogError("there was an issue serializing the data! " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void Load()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _gameManager.collectedCandy = (int) formatter.Deserialize(file);
        }
        catch (SerializationException e)
        {
            Debug.LogError("error deserializing data! " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }
}