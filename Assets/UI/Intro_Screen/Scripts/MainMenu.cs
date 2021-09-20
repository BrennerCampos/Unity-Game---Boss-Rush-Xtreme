using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject continueButton;
    public string startScene, continueScene;

    // Start is called before the first frame update
    void Start()
    {
        // If we have data saved already upon starting the game (any courses unlocked)
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            // Have a continue button option on the main screen
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // If starting a new game, load the start scene
        SceneManager.LoadScene(startScene);
        // And delete all previous saved data
        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

}
