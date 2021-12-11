using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsScreenManager : MonoBehaviour
{

    private bool selected, inputBool;
    public GameObject TxMenu;
    public Image fadeScreen;
    private Tween selectTween;

    // Start is called before the first frame update
    void Start()
    {
        fadeScreen.color = new Color(0, 0, 0, 1);
        AudioManager.instance.BGM.volume = 0.25f;
        AudioManager.instance.FadeInBGM();
        UIController.instance.FadeFromBlack();
        AudioManager.instance.PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && !inputBool)
        {
            inputBool = true;
            AudioManager.instance.PlaySFX_NoPitchFlux(120);
            TxMenu.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (inputBool)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(22);

                fadeScreen.color = new Color(0, 0, 0, 0);
                UIController.instance.fadeSpeed = 2f;
                UIController.instance.FadeToBlack();
                AudioManager.instance.FadeOutBGM();

                StateNameController.ComingFromScene = "ControlsScreen";

                selectTween = DOVirtual.DelayedCall(1, () =>
                {
                    AudioManager.instance.PlaySFX_NoPitchFlux(22);
                    SceneManager.LoadScene("IntroScreen");
                }, false);
            }
        }

    }
}
