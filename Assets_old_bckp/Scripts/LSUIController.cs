using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{

    public static LSUIController instance;
    public GameObject levelInfoPanel;
    public Image fadeScreen;
    public Text levelName, gemsFound, gemsTarget, timeBest, timeTarget;
    public float fadeSpeed;

    private bool shouldFadeToBlack, shouldFadeFromBlack;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            // Takes the alpha value of our black fade panel and move it towards full alpha (black screen) by 1/3rd of a second
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
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

    public void ShowInfo(MapPoint levelInfo)
    {
        // Setting up our Level Info Panel with all the current variables
        
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.gemsTotal;

        // If level has no best time registered...
        if (levelInfo.timeBest == 0)
        {
            timeBest.text = "BEST: ---";
            timeTarget.text = "TARGET: " + levelInfo.timeTarget.ToString("F2") + "s";
        }
        else    // If it does has a time registered...
        {
            timeBest.text = "BEST: " + levelInfo.timeBest.ToString("F2") + "s";
            timeTarget.text = "TARGET: " + levelInfo.timeTarget.ToString("F2") + "s";
        }
        
        // Turn Info Panel on
        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }



}
