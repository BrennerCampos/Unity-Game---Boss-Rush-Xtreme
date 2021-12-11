using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScreenManager : MonoBehaviour
{

    private int currentSelectionID;
    private Animator playerDummyAnimator;
    public GameObject TxStart, TxControls, TxHighScores, TxExit, playerDummy;
    public Image fadeScreen;
    public Transform dummyPositionStart, dummyPositionControls, dummyPositionHighScores, dummyPositionExit;
    private bool selectedBool;
    private Tween selectTween;
   


    private void Awake()
    {
        

    }


    // Start is called before the first frame update
    void Start()
    {
        fadeScreen.color = new Color(255, 255, 255, 1);
        AudioManager.instance.BGM.volume = 0.25f;
        AudioManager.instance.FadeInBGM();
        UIController.instance.FadeFromBlack();
        currentSelectionID = 0;
        playerDummyAnimator = playerDummy.GetComponent<Animator>();
        AudioManager.instance.PlayBGM();

    }

    // Update is called once per frame
    void Update()
    {

        if (!selectedBool)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentSelectionID++;

                AudioManager.instance.PlaySFX_NoPitchFlux(120);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentSelectionID--;

                AudioManager.instance.PlaySFX_NoPitchFlux(120);
            }

            if (currentSelectionID > 3)
            {
                currentSelectionID = 0;
            }

            if (currentSelectionID < 0)
            {
                currentSelectionID = 3;
            }


            if (currentSelectionID == 0)
            {
                // "Start" option
                playerDummy.transform.position = new Vector3(dummyPositionStart.transform.position.x, dummyPositionStart.transform.position.y);
                TxStart.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxControls.GetComponent<SpriteRenderer>().color = Color.white;
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.white;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "IntroScreen";

            }
            else if (currentSelectionID == 1)
            {
                // "Controls" option
                playerDummy.transform.position = new Vector3(dummyPositionControls.transform.position.x, dummyPositionControls.transform.position.y);
                TxControls.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.white;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "IntroScreen";

            }
            else if (currentSelectionID == 2)
            {
                // "High Scores" option
                playerDummy.transform.position = new Vector3(dummyPositionHighScores.transform.position.x, dummyPositionHighScores.transform.position.y);
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxControls.GetComponent<SpriteRenderer>().color = Color.white;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "IntroScreen";

            }
            else if (currentSelectionID == 3)
            {
                // "Exit" option
                playerDummy.transform.position = new Vector3(dummyPositionExit.transform.position.x, dummyPositionExit.transform.position.y);
                TxExit.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxControls.GetComponent<SpriteRenderer>().color = Color.white;
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.white;

                StateNameController.ComingFromScene = "IntroScreen";

            }


            if (Input.GetKeyDown(KeyCode.Space) && (!Input.GetKeyDown(KeyCode.DownArrow) ||
                !Input.GetKeyDown(KeyCode.UpArrow)))
            {

                AudioManager.instance.PlaySFX_NoPitchFlux(22);
                playerDummyAnimator.SetBool("optionSelected", true);
                selectedBool = true;

                fadeScreen.color = new Color(0, 0, 0, 0);
                UIController.instance.fadeSpeed = 1.5f;
                UIController.instance.FadeToBlack();
                AudioManager.instance.FadeOutBGM();

                /*audioFadeTween = DOVirtual.DelayedCall(0.1f, () =>
                {
                    var timer = timeToFade;

                    while (timer > 0)
                    {
                        timer -= Time.deltaTime;

                        if (timer <= 0)
                        {
                            BGM.Stop();
                        }
                        else
                        {
                            BGM.volume = 0.5f;
                        }
                    }
                }, false);*/

                StateNameController.ComingFromScene = "IntroScreen";

                selectTween = DOVirtual.DelayedCall(2, () =>
                {
                    if (currentSelectionID == 0)
                    {
                        // "Start" option
                        SceneManager.LoadScene("LevelSelector");

                    }
                    else if (currentSelectionID == 1)
                    {
                        // "Controls" option
                        // @TODO Load Controls Screen
                        SceneManager.LoadScene("ControlsScreen");

                    }
                    else if (currentSelectionID == 2)
                    {
                        // "High Scores" option
                        // @TODO Load High Scores Screen
                        SceneManager.LoadScene("HighScoreScreen");

                    }
                    else if (currentSelectionID == 3)
                    {
                        // @TODO Exits Program
                        // "Exit" option

                        Application.Quit();
                        Debug.Log("Exiting Game.");
                    }
                }, false);
            }
        }
    }
}
