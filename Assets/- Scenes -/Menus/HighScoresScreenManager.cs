using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoresScreenManager : MonoBehaviour
{

    public GameObject TxBack, TxNext;
    private GameObject TxDummy;
    private bool inputBool;
    public Image fadeScreen;
    private Tween selectTween;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM();

        if (StateNameController.ComingFromScene == "IntroScreen")
        {
            TxBack.SetActive(true);
            TxNext.SetActive(false);
            TxDummy = TxBack;
        } else
        {
            TxNext.SetActive(true);
            TxBack.SetActive(false);
            TxDummy = TxNext;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!inputBool)
        {
            if (Input.anyKey)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(120);
                TxDummy.GetComponent<SpriteRenderer>().color = Color.yellow;
                inputBool = true;
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.Home) && Input.GetKeyDown(KeyCode.Home))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("All PlayerPrefs deleted.");
            }

            
            if (Input.GetKeyDown(KeyCode.Space) &&
                (!Input.GetKeyDown(KeyCode.DownArrow) ||
                 !Input.GetKeyDown(KeyCode.UpArrow) ||
                 !Input.GetKeyDown(KeyCode.LeftArrow) ||
                 !Input.GetKeyDown(KeyCode.RightArrow)))
            {

                AudioManager.instance.PlaySFX_NoPitchFlux(22);

                fadeScreen.color = new Color(0, 0, 0, 0);
                UIController.instance.fadeSpeed = 1.5f;
                UIController.instance.FadeToBlack();
                AudioManager.instance.FadeOutBGM();



                selectTween = DOVirtual.DelayedCall(2, () =>
                {
                    if (StateNameController.ComingFromScene == "IntroScreen")
                    {
                        StateNameController.ComingFromScene = "HighScoreScreen";
                        SceneManager.LoadScene("IntroScreen");
                    }
                    else
                    {
                        StateNameController.ComingFromScene = "HighScoreScreen";
                        SceneManager.LoadScene("ThanksForPlaying");
                    }
                }, false);
            }
        }
    }
}
