using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public Animator playerAnimator, bossAnimator;
    public Transform bossSpawnPoint;
    public GameObject boss;
    public float waitToRespawn, timeInLevel, timeToWaitBeforeStart, waitforBossSpawnTime;
    public string levelToLoad;
    public bool levelEndedBool;
    private string currentBoss;

    private Tween standingTween;
    private Tween victoryStanceTween;
    private Tween teleportOutTween;

    // Object Constructor
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
        FirstSpawnBoss();
        AudioManager.instance.FadeInBGM();
        AudioManager.instance.PlayBGM();

        if (boss.GetComponent<DinoRexBoss>() != null)
        {
            Debug.Log("Current Boss = DINO REX");
                currentBoss = "DinoRex";
        }
        else if (boss.GetComponent<BlizzardWolfgangBoss>() != null)
        {
            Debug.Log("Current Boss = BLIZZARD WOLFANG");
            currentBoss = "BlizzardWolfgang";
        }
        else if (boss.GetComponent<CyberPeacockBoss>() != null)
        {
            Debug.Log("Current Boss = CYBER PEACOCK");
            currentBoss = "CyberPeacock";
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;

        if (timeInLevel > timeToWaitBeforeStart && !levelEndedBool
            &&
            FindObjectOfType<DinoRexBoss>() == null          &&
            FindObjectOfType<BlizzardWolfgangBoss>() == null &&
            FindObjectOfType<CyberPeacockBoss>() == null     &&
            FindObjectOfType<CrescentGrizzlyBoss>() == null
            )
        {
            levelEndedBool = true;
            EndLevel();
        }

    }

    public void FirstSpawnBoss()
    {
      StartCoroutine(FirstSpawnBossCo());

    }

    private IEnumerator FirstSpawnBossCo()
    {
        yield return new WaitForSeconds(waitforBossSpawnTime);
        UIController.instance.WarningTime();
        if (FindObjectOfType<CyberPeacockBoss>() != null)
        {
            bossAnimator.SetTrigger("introTeleport");
            boss.transform.position = bossSpawnPoint.position;
        }
    }



    public void RespawnPlayer()
    {
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


    /*public void VictoryStance()
    {
        
        
    }*/

    /*public IEnumerator VictoryStanceCo()
    {
        
        //yield return new WaitForSeconds(4f);
        //playerAnimator.SetTrigger("startVictoryStance");
        //playerAnimator.ResetTrigger("startVictoryStance");

        yield return new WaitForSeconds(1f);
    

    }*/

    public void EndLevel()
    {
        /*//playerAnimator.SetBool("isStandingIdle", false);
        playerAnimator.SetTrigger("standOverride");
        playerAnimator.ResetTrigger("standOverride");
        // CameraController.instance.stopFollow = true;
        playerAnimator.SetBool("isStandingIdle", true);
        // Removes player input ability
        PlayerController.instance.stopInput = true;

        standingTween = DOVirtual.DelayedCall(4f, StartVictoryStance, false);*/

        UIController.instance.GetComponentInChildren<InGameClock>().StopTimer();
        UIController.instance.timeInLevelText.color = Color.red;

        PlayerController.instance.levelEnd = true;
        PlayerController.instance.stopInput = true;
        StartCoroutine(EndLevelCo());
    //StartCoroutine(VictoryStanceCo());
    }

    /*public void StartVictoryStance()
    {
        playerAnimator.SetTrigger("startVictoryStance");
        playerAnimator.ResetTrigger("startVictoryStance");


        victoryStanceTween = DOVirtual.DelayedCall(1.5f, () =>
        {
           teleportOutTween = DOVirtual.DelayedCall(1f, StartTeleportOut, false);
        }, false);

    }
    public void StartTeleportOut()
    {
        standingTween?.Kill();
        playerAnimator.SetTrigger("startTeleportOut");

        /*teleportOutTween = DOVirtual.DelayedCall(1.5f, () =>
        {
            //
        }, false);#1#
    }*/

    public IEnumerator EndLevelCo()
    {
       // Play victory music
        AudioManager.instance.PlayLevelVictory();

        switch (currentBoss)
        {
            case "DinoRex":
                StateNameController.DinoRexDefeated = true;
                PlayerPrefs.SetString("DinoRexFinalScore", UIController.instance.dinoRexScore.text);
                PlayerPrefs.SetString("DinoRexFinalTime", timeInLevel.ToString());
                Debug.Log("Defeated : DINO REX");
                Debug.Log("Final Score : " + PlayerPrefs.GetString("DinoRexFinalScore"));
                Debug.Log("Time in Level : " + PlayerPrefs.GetString("DinoRexFinalTime"));
                break;
            case "BlizzardWolfgang":
                StateNameController.BlizzardWolfgangDefeated = true;
                PlayerPrefs.SetString("BlizzardWolfgangFinalScore", UIController.instance.blizzardWolfangScore.text);
                PlayerPrefs.SetString("BlizzardWolfgangFinalTime", timeInLevel.ToString());
                Debug.Log("Defeated : BLIZZARD WOLFGANG");
                Debug.Log("Final Score : " + PlayerPrefs.GetString("BlizzardWolfgangFinalScore"));
                Debug.Log("Time in Level : " + PlayerPrefs.GetString("BlizzardWolfgangFinalTime"));
                break;
            case "CyberPeacock":
                StateNameController.CyberPeacockDefeated = true;
                PlayerPrefs.SetString("CyberPeacockFinalScore", UIController.instance.cyberPeacockScore.text);
                PlayerPrefs.SetString("CyberPeacockFinalTime", timeInLevel.ToString());
                Debug.Log("Defeated : CYBER PEACOCK");
                Debug.Log("Final Score : " + PlayerPrefs.GetString("CyberPeacockFinalScore"));
                Debug.Log("Time in Level : " + PlayerPrefs.GetString("CyberPeacockFinalTime"));
                break;
        }

        // Wait a bit then fade screen to black
        //yield return new WaitForSeconds(1f);
        //playerAnimator.SetTrigger("startVictoryStance");
        //playerAnimator.ResetTrigger("startVictoryStance");


        yield return new WaitForSeconds(6f);
        UIController.instance.FadeToBlack();

        // Waits an extra amount of time for victory music to finish playing
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 3f);

        // Marks current level as unlocked in PlayerPrefs and gets level name
        //PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        //PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        /*// If there is any GEMS data stored on current level...
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
        }*/

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

        // Finally, loads Level Selection Screen
        SceneManager.LoadScene("LevelSelector");
    }
}



