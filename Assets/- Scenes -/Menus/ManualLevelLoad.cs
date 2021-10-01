using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using UnityEngine;

public class ManualLevelLoad : MonoBehaviour
{

    public GameObject loadingScreen, pauseScreen;
    public PauseMenu pauseMenu;
    public float timeToWait;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu.PauseUnpause();
        StartCoroutine(PausedForLoadingCO());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator PausedForLoadingCO()
    {
         
        yield return new WaitForSeconds(timeToWait);
    }
}
