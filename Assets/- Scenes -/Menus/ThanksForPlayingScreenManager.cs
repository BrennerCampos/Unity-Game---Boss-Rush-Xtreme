using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThanksForPlayingScreenManager : MonoBehaviour
{

    public GameObject TxMenu, TxExit;
    private bool inputBool;
    private int currentSelectionID;
    public Image fadeScreen;
    private Tween selectTween;

    // Start is called before the first frame update
    void Start()
    {
        fadeScreen.color = new Color(0, 0, 0, 1);
        //AudioManager.instance.BGM.volume = 0.25f;
        //AudioManager.instance.FadeInBGM();
        UIController.instance.FadeFromBlack();
        currentSelectionID = 0;
        AudioManager.instance.PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {

        if (!inputBool)
        {
            if (Input.anyKey)
            {
                currentSelectionID = 0;
                AudioManager.instance.PlaySFX_NoPitchFlux(120);
                TxMenu.GetComponent<SpriteRenderer>().color = Color.yellow;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;
                inputBool = true;
            }
        }
        else
        {       // if MENU is selected
            if (currentSelectionID == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentSelectionID = 1;
                    AudioManager.instance.PlaySFX_NoPitchFlux(120);
                    TxMenu.GetComponent<SpriteRenderer>().color = Color.white;
                    TxExit.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    fadeScreen.color = new Color(0, 0, 0, 0);
                    UIController.instance.fadeSpeed = 1.5f;
                    UIController.instance.FadeToBlack();
                    AudioManager.instance.FadeOutBGM();
                    AudioManager.instance.PlaySFX_NoPitchFlux(22);

                    StateNameController.ComingFromScene = "ThanksForPlaying";
                    
                    StateNameController.DinoRexDefeated = false;
                    StateNameController.BlizzardWolfgangDefeated = false;
                    StateNameController.CyberPeacockDefeated = false;

                    selectTween = DOVirtual.DelayedCall(3, () =>
                    {
                        SceneManager.LoadScene("IntroScreen");
                    }, false);
                }
            }
            else // if EXIT is selected
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentSelectionID = 0;
                    AudioManager.instance.PlaySFX_NoPitchFlux(120);
                    TxMenu.GetComponent<SpriteRenderer>().color = Color.yellow;
                    TxExit.GetComponent<SpriteRenderer>().color = Color.white;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {

                    fadeScreen.color = new Color(0, 0, 0, 0);
                    UIController.instance.fadeSpeed = 1.5f;
                    UIController.instance.FadeToBlack();
                    AudioManager.instance.FadeOutBGM();
                    AudioManager.instance.PlaySFX_NoPitchFlux(22);

                    StateNameController.ComingFromScene = "ThanksForPlaying";
                    Debug.Log("Exiting Game...");

                    selectTween = DOVirtual.DelayedCall(3, () =>
                    {

                        Application.Quit();
                    }, false);
                }
            }
            

        }

    }
}
