using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public Text bossHP;
    public Image fadeScreen;
    public Image WarningSign;
    public Text dinoRexScore, blizzardWolfangScore, cyberPeacockScore;
    public Text currentScore, timeInLevelText;
    public GameObject loadingScreen;
    public float fadeSpeed, warningSpeed;
    public int cycleCount;
    
    private bool shouldFadeToBlack, shouldFadeFromBlack, warningTime, reverseCycle;


    // Creates UI Controller constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //loadingScreen.SetActive(false);
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1);
        if (timeInLevelText != null)
        {
            timeInLevelText.color = Color.white;
        }
       
        UpdateBossHP();
        FadeFromBlack();

        if (FindObjectOfType<DinoRexBoss>())
        {
            dinoRexScore.text = "0";
            currentScore = dinoRexScore;
        }
        else if (FindObjectOfType<BlizzardWolfgangBoss>())
        {
            blizzardWolfangScore.text = "0";
            currentScore = blizzardWolfangScore;
        }
        else if (FindObjectOfType<CyberPeacockBoss>())
        {
            cyberPeacockScore.text = "0";
            currentScore = cyberPeacockScore;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        if (warningTime)
        {
            if (reverseCycle)
            {
                WarningSign.color = new Color(WarningSign.color.r, WarningSign.color.g, WarningSign.color.b,
                    Mathf.MoveTowards(WarningSign.color.a, 0f, warningSpeed * Time.deltaTime));

                if (WarningSign.color.a == 0f)
                {
                    reverseCycle = false;
                    cycleCount--;
                }
            }
            else
            {
                WarningSign.color = new Color(WarningSign.color.r, WarningSign.color.g, WarningSign.color.b,
                    Mathf.MoveTowards(WarningSign.color.a, 1f, warningSpeed * Time.deltaTime));
                
                if (WarningSign.color.a == 1f)
                {
                    reverseCycle = true;
                }
            }
            
            if (cycleCount <= 0)
            {
                warningTime = false;
                WarningSign.gameObject.SetActive(false);
                Destroy(WarningSign.gameObject);
                GetComponentInChildren<InGameClock>().StartTimer();
            }
        }




        if (shouldFadeToBlack)
        {
            // Takes the alpha value of our black fade panel and move it towards full alpha (black screen) by 1/3rd of a second
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed*Time.deltaTime));
            // If completely black
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            // Takes the alpha value of our black fade panel and move it towards 0 alpha (transparent screen) by 1/3rd of a second
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            // If completely black
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }


    }

    // Refresh our Health UI after any changes
    public void UpdateHealthDisplay()
    {
        /*// Switches how many hearts are drawn based on Player's current health
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }*/
    }

    // Refreshes our UI Gem count text
    public void UpdateBossHP()
    {
        //bossHP.text = FindObjectOfType<DinoRexBoss>().currentHealth.ToString();
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void WarningTime()
    {
        WarningSign.gameObject.SetActive(true);
        warningTime = true;
    }

}
