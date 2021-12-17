using System.Collections;
using System.Collections.Generic;
using Core.AI;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySFX : EnemyAction
{
    public AudioManager audioManager;
    public int type, soundToPlay;


    public override void OnStart()
    {
        base.OnStart();
        PlaySFX_Type(type, soundToPlay);
    }


    public void PlaySFX_Type(int type, int soundToPlay)
    {
        switch (type)
        {
            case 0:     // Regular Slight Pitch Mod SFX
                audioManager.PlaySFX(soundToPlay);
                break;
            case 1:     // High Pitched SFX
                audioManager.PlaySFX_HighPitch(soundToPlay);
                break;
            case 2:     // Don't Stop Other Same Sounds
                audioManager.PlaySFXOverlap(soundToPlay);
                break;
            case 3:     // Don't Randomize Pitch
                audioManager.PlaySFX_NoPitchFlux(soundToPlay);
                break;
            default:
                audioManager.PlaySFX_NoPitchFlux(soundToPlay);
                break;
        }

    }
}
