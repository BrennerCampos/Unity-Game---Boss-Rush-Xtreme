using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public float waitToRespawn, timeInLevel;
    public int gemsCollected;
    public string levelToLoad;


    // Object Constructor
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        // Coroutines must be started with "StartCoroutine"
        StartCoroutine(RespawnCo());
    }

    // CoRoutine
    private IEnumerator RespawnCo()
    {
        // Deactivates our player
        PlayerController.instance.gameObject.SetActive(false);
        
        // Plays 'Death' SFX
        AudioManager.instance.PlaySFX(8);

        // Pauses the further execution of the script until our waitToRespawn timer is complete (minus a fraction of our fade speed)
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();

        // Makes it wait for amount of time it should take to fade + a bit of a buffer so it stays fully black for a fraction of a second
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 0.2f);
        UIController.instance.FadeFromBlack();

        // Reactivates our player
        PlayerController.instance.gameObject.SetActive(true);
        // Spawns our player's position at our last checkpoint
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        // Resets our player health to be full...
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        
        // And updates our UI accordingly
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
       // Play victory music
        AudioManager.instance.PlayLevelVictory();
        
        // Removes player input ability
        PlayerController.instance.stopInput = true;
        CameraController.instance.stopFollow = true;
       
        UIController.instance.levelCompleteText.SetActive(true);

        // Wait a bit then fade screen to black
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();

        // Waits an extra amount of time for victory music to finish playing
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 3f);

        // Marks current level as unlocked in PlayerPrefs and gets level name
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        // If there is any GEMS data stored on current level...
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            // If current gems total is better than previous best (or goal)
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected))
            {
                // Sets PlayerPref's gemsCollected for course to our current gem count
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else    // If no GEMS data is found...
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        // If there is any TIME data stored on current level...
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            // If current time taken on level is better than previous best time...
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else    // If no TIME data is found...
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        // Finally, loads next scene
        SceneManager.LoadScene(levelToLoad);
    }
}



