using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    public GameObject gemBadge, timeBadge;
    public MapPoint up, right, down, left;
    public String levelToLoad, levelToCheck, levelName;
    public int gemsCollected, gemsTotal;
    public float timeBest, timeTarget;
    public bool isLevel, isLocked;
    

    // Start is called before the first frame update
    void Start()
    {
        // If current Map Point is a level and we are not loading any new level
        if(isLevel && levelToLoad != null)
        {
            // If the current level has a best gems total registered...
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            // If the current level has a best time registered...
            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                timeBest = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            // If we get at or above our target 'gemsTotal', display a gem badge denoting achievement
            if (gemsCollected >= gemsTotal)
            {
                gemBadge.SetActive(true);
            }
            
            // If our time is better than target time and level has been played, display a clock badge
            if (timeBest <= timeTarget && timeBest != 0)
            {
                timeBadge.SetActive(true);
            }

            // Lock the level initially by default
            isLocked = true;
            
            // If we have a level in question...
            if (levelToCheck != null)
            {
                // If there is any value stored in Player Prefs (for level being unlocked)...
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    // If it is marked as unlocked (1)...
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        // Set it to be unlocked
                        isLocked = false;
                    }
                }
            }

            // Also check if both values are the same (If "Level_1-1" == "Level_1-1")
            // (Prevents problems from not having level 1 completed)
            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
