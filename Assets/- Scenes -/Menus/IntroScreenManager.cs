using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreenManager : MonoBehaviour
{

    private int currentSelectionID;
    private Animator playerDummyAnimator;
    public GameObject TxStart, TxControls, TxHighScores, TxExit, playerDummy;
    public Transform dummyPositionStart, dummyPositionControls, dummyPositionHighScores, dummyPositionExit;
    private bool selectedBool;


    // Start is called before the first frame update
    void Start()
    {

        currentSelectionID = 0;
        playerDummyAnimator = playerDummy.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!selectedBool)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentSelectionID++;

                // Play SFX

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentSelectionID--;

                // Play SFX
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

            }
            else if (currentSelectionID == 1)
            {
                // "Controls" option
                playerDummy.transform.position = new Vector3(dummyPositionControls.transform.position.x, dummyPositionControls.transform.position.y);
                TxControls.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.white;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;

            }
            else if (currentSelectionID == 2)
            {
                // "High Scores" option
                playerDummy.transform.position = new Vector3(dummyPositionHighScores.transform.position.x, dummyPositionHighScores.transform.position.y);
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxControls.GetComponent<SpriteRenderer>().color = Color.white;
                TxExit.GetComponent<SpriteRenderer>().color = Color.white;

            }
            else if (currentSelectionID == 3)
            {
                // "Exit" option
                playerDummy.transform.position = new Vector3(dummyPositionExit.transform.position.x, dummyPositionExit.transform.position.y);
                TxExit.GetComponent<SpriteRenderer>().color = Color.yellow;

                TxStart.GetComponent<SpriteRenderer>().color = Color.white;
                TxControls.GetComponent<SpriteRenderer>().color = Color.white;
                TxHighScores.GetComponent<SpriteRenderer>().color = Color.white;

            }


            if (Input.GetKeyDown(KeyCode.Space) && (!Input.GetKeyDown(KeyCode.DownArrow) ||
                !Input.GetKeyDown(KeyCode.UpArrow)))
            {

                playerDummyAnimator.SetBool("optionSelected", true);
                selectedBool = true;

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
            }
        }
        
    }
}
