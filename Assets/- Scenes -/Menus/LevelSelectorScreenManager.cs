using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorScreenManager : MonoBehaviour
{


    private int currentSelectionID;
    public bool selectedBool, dinoRexDefeated, blizzardWolfangDefeated, cyberPeacockDefeated;
    public GameObject TxDinoRex, TxBlizzardWolfang, TxCyberPeacock, TxMenu, selectedBorderGold;

    public GameObject dinoRexPortraitEnabled,
        dinoRexPortraitDisabled,
        blizzardWolfangPortraitEnabled,
        blizzardWolfangPortraitDisabled,
        cyberPeacockPortraitEnabled,
        cyberPeacockPortraitDisabled;
    public Transform dinoRexBorderPos, blizzardWolfangBorderPos, cyberPeacockBorderPos;

    public Image fadeScreen;
    private Tween selectTween;
    private bool menuSelectedBool;

    // Start is called before the first frame update
    void Start()
    {

        dinoRexDefeated = StateNameController.DinoRexDefeated;
        blizzardWolfangDefeated = StateNameController.BlizzardWolfgangDefeated;
        cyberPeacockDefeated = StateNameController.CyberPeacockDefeated;

        TxMenu.GetComponent<SpriteRenderer>().color = Color.white;
        fadeScreen.color = new Color(0, 0, 0, 1);
        AudioManager.instance.BGM.volume = 0.25f;
        AudioManager.instance.FadeInBGM();
        UIController.instance.FadeFromBlack();

        AudioManager.instance.PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {

        // Dino Rex Portrait
        if (!dinoRexDefeated)
        {
            dinoRexPortraitEnabled.SetActive(true);
            dinoRexPortraitDisabled.SetActive(false);
        }
        else
        {
            dinoRexPortraitEnabled.SetActive(false);
            dinoRexPortraitDisabled.SetActive(true);
        }

        // Blizzard Wolfang Portrait
        if (!blizzardWolfangDefeated)
        {
            blizzardWolfangPortraitEnabled.SetActive(true);
            blizzardWolfangPortraitDisabled.SetActive(false);
        }
        else
        {
            blizzardWolfangPortraitEnabled.SetActive(false);
            blizzardWolfangPortraitDisabled.SetActive(true);
        }

        // Cyber Peacock Portrait
        if (!cyberPeacockDefeated)
        {
            cyberPeacockPortraitEnabled.SetActive(true);
            cyberPeacockPortraitDisabled.SetActive(false);
        }
        else
        {
            cyberPeacockPortraitEnabled.SetActive(false);
            cyberPeacockPortraitDisabled.SetActive(true);
        }




        if (Input.GetKeyDown(KeyCode.LeftArrow) && !menuSelectedBool)
        {
            currentSelectionID--;
            AudioManager.instance.PlaySFX_NoPitchFlux(120);


        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !menuSelectedBool)
        {
            currentSelectionID++;
            AudioManager.instance.PlaySFX_NoPitchFlux(120);
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !menuSelectedBool)
        {
            AudioManager.instance.PlaySFX_NoPitchFlux(120);
            menuSelectedBool = true;

            TxDinoRex.GetComponent<SpriteRenderer>().color = Color.white;
            TxBlizzardWolfang.GetComponent<SpriteRenderer>().color = Color.white;
            TxCyberPeacock.GetComponent<SpriteRenderer>().color = Color.white;
            TxDinoRex.transform.localScale = new Vector3(1, 1, 1);
            TxBlizzardWolfang.transform.localScale = new Vector3(1, 1, 1);
            TxCyberPeacock.transform.localScale = new Vector3(1, 1, 1);
            
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow) && menuSelectedBool)
        {
            AudioManager.instance.PlaySFX_NoPitchFlux(120);
            menuSelectedBool = false;
            TxMenu.GetComponent<SpriteRenderer>().color = Color.white;
        }

        

        if (currentSelectionID > 2)
        {
            currentSelectionID = 0;
        }

        if (currentSelectionID < 0)
        {
            currentSelectionID = 2;
        }



        if (!menuSelectedBool)
        {

            if (currentSelectionID == 0)
            {
                // "Dino Rex" option
                selectedBorderGold.transform.position = new Vector3(dinoRexBorderPos.transform.position.x,
                    dinoRexBorderPos.transform.position.y);

                TxDinoRex.GetComponent<SpriteRenderer>().color = Color.yellow;
                TxDinoRex.transform.localScale = new Vector3(1.1f, 1.1f, 1);

                TxBlizzardWolfang.GetComponent<SpriteRenderer>().color = Color.white;
                TxCyberPeacock.GetComponent<SpriteRenderer>().color = Color.white;
                TxBlizzardWolfang.transform.localScale = new Vector3(1, 1, 1);
                TxCyberPeacock.transform.localScale = new Vector3(1, 1, 1);
                //TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "LevelSelector";

            }
            else if (currentSelectionID == 1)
            {
                // "Blizzard Wolfgang" option
                selectedBorderGold.transform.position = new Vector3(blizzardWolfangBorderPos.transform.position.x,
                    blizzardWolfangBorderPos.transform.position.y);
                TxBlizzardWolfang.GetComponent<SpriteRenderer>().color = Color.yellow;
                TxBlizzardWolfang.transform.localScale = new Vector3(1.1f, 1.1f, 1);

                TxDinoRex.GetComponent<SpriteRenderer>().color = Color.white;
                TxCyberPeacock.GetComponent<SpriteRenderer>().color = Color.white;
                TxDinoRex.transform.localScale = new Vector3(1, 1, 1);
                TxCyberPeacock.transform.localScale = new Vector3(1, 1, 1);
                //TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "LevelSelector";

            }
            else if (currentSelectionID == 2)
            {
                // "Cyber Peacock" option
                selectedBorderGold.transform.position = new Vector3(cyberPeacockBorderPos.transform.position.x,
                    cyberPeacockBorderPos.transform.position.y);
                TxCyberPeacock.GetComponent<SpriteRenderer>().color = Color.yellow;
                TxCyberPeacock.transform.localScale = new Vector3(1.1f, 1.1f, 1);

                TxDinoRex.GetComponent<SpriteRenderer>().color = Color.white;
                TxBlizzardWolfang.GetComponent<SpriteRenderer>().color = Color.white;
                TxDinoRex.transform.localScale = new Vector3(1, 1, 1);
                TxBlizzardWolfang.transform.localScale = new Vector3(1, 1, 1);
                //TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "LevelSelector";
            }

            if (Input.GetKeyDown(KeyCode.Space) &&
                (!Input.GetKeyDown(KeyCode.DownArrow) ||
                 !Input.GetKeyDown(KeyCode.UpArrow) ||
                 !Input.GetKeyDown(KeyCode.LeftArrow) ||
                 !Input.GetKeyDown(KeyCode.RightArrow)))
            {

                if (currentSelectionID == 0 && !dinoRexDefeated ||
                    currentSelectionID == 1 && !blizzardWolfangDefeated ||
                    currentSelectionID == 2 && !cyberPeacockDefeated)
                    {
                        //playerDummyAnimator.SetBool("optionSelected", true);
                        selectedBool = true;
                        AudioManager.instance.PlaySFX_NoPitchFlux(22);


                        fadeScreen.color = new Color(0, 0, 0, 0);
                        UIController.instance.fadeSpeed = 1.5f;
                        UIController.instance.FadeToBlack();
                        AudioManager.instance.FadeOutBGM();


                        selectTween = DOVirtual.DelayedCall(3, () =>
                        {
                            if (currentSelectionID == 0 && !dinoRexDefeated)
                            {
                                // "Dino Rex" option
                                SceneManager.LoadScene("DinoRexPreviewScreen");

                            }
                            else if (currentSelectionID == 1 && !blizzardWolfangDefeated)
                            {
                                // "Blizzard Wolfgang" option
                                SceneManager.LoadScene("BlizzardWolfgangPreviewScreen");

                            }
                            else if (currentSelectionID == 2 && !cyberPeacockDefeated)
                            {
                                // "Cyber Peacock" option
                                SceneManager.LoadScene("CyberPeacockPreviewScreen");
                            }
                        }, false);

                    }
                else
                {
                    // Invalid Selection Sound
                    AudioManager.instance.PlaySFX_NoPitchFlux(85);
                }
            }
        }
        else
        {

            selectedBorderGold.transform.position = new Vector3(4000,
                4000);
            TxMenu.GetComponent<SpriteRenderer>().color = Color.yellow;


            if (Input.GetKeyDown(KeyCode.Space) &&
                (!Input.GetKeyDown(KeyCode.DownArrow) ||
                 !Input.GetKeyDown(KeyCode.UpArrow) ||
                 !Input.GetKeyDown(KeyCode.LeftArrow) ||
                 !Input.GetKeyDown(KeyCode.RightArrow)))
            {
                selectedBool = true;
                AudioManager.instance.PlaySFX_NoPitchFlux(22);


                fadeScreen.color = new Color(0, 0, 0, 0);
                UIController.instance.fadeSpeed = 1.5f;
                UIController.instance.FadeToBlack();
                AudioManager.instance.FadeOutBGM();


                selectTween = DOVirtual.DelayedCall(2, () =>
                {
                    SceneManager.LoadScene("IntroScreen");
                }, false);
            }
        }


        if (dinoRexDefeated && blizzardWolfangDefeated && cyberPeacockDefeated)
        {
            StateNameController.ComingFromScene = "LevelSelector";
            SceneManager.LoadScene("HighScoreScreen");
        }

    }
}

