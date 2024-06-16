using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for manipulating UI elements like Text

public class GameTimer : MonoBehaviour
{
    public Text timerText; // Assign this in the inspector
    public float finalScore;
    private float startTime;
    private bool timerActive = false;

    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        timerActive = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int) t / 60).ToString("00");
            string seconds = (t % 60).ToString("00.00");
            finalScore = t;
            timerText.text = minutes + ":" + seconds;
        }
    }
}
