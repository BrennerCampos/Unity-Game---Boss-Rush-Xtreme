using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{

    public LSPlayer thePlayer;

    private MapPoint[] allPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        // Put all Map Points in an array
        allPoints = FindObjectsOfType<MapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            // For each Point...
            foreach (MapPoint point in allPoints)
            {
                // If the point is our current level...
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    // Places Player icon to be at the current level's position marker
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        // Plays "Level Selected" SFX & fade to black
        AudioManager.instance.PlaySFX(4);
        LSUIController.instance.FadeToBlack();

        // Waits for the amount of time our fade speed + a bit longer
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) + 0.25f);

        // Loads our level
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
