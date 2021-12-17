using Core.AI;

public class StopSFX : EnemyAction
{
    public AudioManager audioManager;
    public int soundToStop;

    public override void OnStart()
    {
        base.OnStart();
        StopSFX_Normal(soundToStop);
    }

    public void StopSFX_Normal(int soundToStop)
    {
        audioManager.StopSFX(soundToStop);
    }
}