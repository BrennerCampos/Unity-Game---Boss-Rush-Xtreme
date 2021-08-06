using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public GameObject levelCompleteText;
    public Text bossHP;
    public Image fadeScreen;
    public float fadeSpeed;
    
    private bool shouldFadeToBlack, shouldFadeFromBlack;


    // Creates UI Controller constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateBossHP();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<DinoRexBoss>())
        {
            UpdateBossHP();
        }
        else
        { 
            //bossHP.text = "0";
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
}
