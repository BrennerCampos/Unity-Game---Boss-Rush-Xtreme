using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffects;
    public AudioSource BGM, levelEndMusic, bossMusic;
    

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
        
    }

    public void PlaySFX(int soundToPlay)
    {
        // First, stop any other already playing instance of this sound effect
        soundEffects[soundToPlay].Stop();
        // Randomizing pitch of SFX slightly so it doesn't sound monotonous and robotic
        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
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
        bossMusic.Play();
    }

    public void StopBossMusic()
    {
        bossMusic.Stop();
        BGM.Play();
    }

}
