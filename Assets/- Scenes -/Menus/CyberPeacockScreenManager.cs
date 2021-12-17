using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CyberPeacockScreenManager : MonoBehaviour
{
    public float previewTimer, startPreviewTimer;
    public Image fadeScreen;
    private Tween delayTween;
    private bool loadFlag;

    // Start is called before the first frame update
    void Start()
    {

        // Play Intro Music
        previewTimer = startPreviewTimer;

        fadeScreen.color = new Color(255, 255, 255, 1);
        UIController.instance.fadeSpeed = 0.5f;
        //AudioManager.instance.BGM.volume = 0.25f;
        //AudioManager.instance.FadeInBGM();
        UIController.instance.FadeFromBlack();
        AudioManager.instance.PlayBGM();

    }

    // Update is called once per frame
    void Update()
    {
        previewTimer -= Time.deltaTime;

        // FADE INTO

        if (previewTimer <= 0 && !loadFlag)
        {
            fadeScreen.color = new Color(0, 0, 0, 0);
            UIController.instance.fadeSpeed = 1.5f;
            UIController.instance.FadeToBlack();
            AudioManager.instance.FadeOutBGM();
            loadFlag = true;
            delayTween = DOVirtual.DelayedCall(1.5f, () =>
            {
                SceneManager.LoadScene("Boss_Cyber_Peacock_Stage");
                delayTween.Kill();
            }, false);
        }
    }
}
