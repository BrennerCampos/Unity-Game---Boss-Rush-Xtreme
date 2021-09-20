using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{

    public GameObject theBossBattle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Activates the boss along with other related elements
            theBossBattle.SetActive(true);

            // Deactivates the trigger area
            gameObject.SetActive(false);

            // Starts boss music and stops regular level music
            AudioManager.instance.PlayBossMusic();
        }
    }
}
