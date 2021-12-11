using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffects;
    public AudioSource BGM, levelEndMusic;
    private bool shouldFadeOut, shouldFadeIn;
    public float timer;
    

    // Creates an AudioManager instance constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (shouldFadeOut)
        {

            BGM.volume = Mathf.MoveTowards(BGM.volume, 0f, 0.45f * Time.deltaTime);
            //Debug.Log("volume" + BGM.volume);

            if (BGM.volume <= 0)
            {
                shouldFadeOut = false;
            }

        }

        if (shouldFadeIn)
        {
            BGM.volume = Mathf.MoveTowards(BGM.volume, 1f, 2 * Time.deltaTime);
            //Debug.Log("volume" + BGM.volume);

            if (BGM.volume >= 1 )
            {
                shouldFadeIn = false;
            }
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        // First, stop any other already playing instance of this sound effect
        soundEffects[soundToPlay].Stop();
        // Randomizing pitch of SFX slightly so it doesn't sound monotonous and robotic
        soundEffects[soundToPlay].pitch = Random.Range(0.95f, 1.05f);
        // Then play the sound effect from the array
        soundEffects[soundToPlay].Play();
    }

    public void PlaySFXOverlap(int soundToPlay)
    {
        // Randomizing pitch of SFX slightly so it doesn't sound monotonous and robotic
        soundEffects[soundToPlay].pitch = Random.Range(0.95f, 1.05f);
        // Then play the sound effect from the array
        soundEffects[soundToPlay].Play();
    }


    public void PlaySFX_HighPitch(int soundToPlay)
    {
        // First, stop any other already playing instance of this sound effect
        soundEffects[soundToPlay].Stop();
        // Randomizing pitch of SFX slightly so it doesn't sound monotonous and robotic
        soundEffects[soundToPlay].pitch = 1.10f;
        // Then play the sound effect from the array
        soundEffects[soundToPlay].Play();
    }

    public void PlaySFX_NoPitchFlux(int soundToPlay)
    {
        // First, stop any other already playing instance of this sound effect
        soundEffects[soundToPlay].Stop();
        // Pitch is normal
        soundEffects[soundToPlay].pitch = 1f;
        // Then play the sound effect from the array
        soundEffects[soundToPlay].Play();
    }


    public void PlayLevelVictory()
    {
        // First, stop background music
        BGM.Stop();
        // The play end music
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        BGM.Stop();
    }

    public void PlayBGM()
    {
        BGM.Stop();
        BGM.Play();
    }

    public void StopSFX(int sfxToStop)
    {
        soundEffects[sfxToStop].Stop();
    }
    public void StopBGM()
    {
        BGM.Stop();
    }


    public void FadeOutBGM()
    {
        shouldFadeOut = true;
        shouldFadeIn = false;
    }

    public void FadeInBGM()
    {
        shouldFadeIn = true;
        shouldFadeOut = false;
    }
}
