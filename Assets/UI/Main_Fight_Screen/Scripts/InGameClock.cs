using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System;

public class InGameClock : MonoBehaviour
{
    private Text textClock;
    private bool timerActive = false;
    private float currentTime;
    public int startMinutes, startSeconds, startMilliseconds;

    void Start()
    {
        textClock = GetComponent<Text>();
        currentTime = 0;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        textClock.text = time.ToString(@"mm\:ss\:fff");
    }
    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}