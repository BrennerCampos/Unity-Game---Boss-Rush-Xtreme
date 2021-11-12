using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu instance;
    public GameObject pauseScreen;
    public string levelSelect, mainMenu;
    public bool isPaused;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);

            // Setting how fast time is passing in Unity/Game to 1 (normal speed)
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            
            // Setting how fast time is passing in Unity/Game to 0 (stop)
            Time.timeScale = 0f;
        }
    }

    public void LevelSelect()
    {
        // Marks the level as "CurrentLevel" in the PlayerPrefs, then loads the level
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(levelSelect);

        // Setting how fast time is passing in Unity/Game to 1 (normal speed)
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);

        // Setting how fast time is passing in Unity/Game to 1 (normal speed)
        Time.timeScale = 1f;
    }

}
