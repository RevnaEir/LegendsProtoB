using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;
    private float startTime;
    private bool raceActive = false;
    private float penaltyTime = 0;

    void Awake()
    {
        Instance = this;
    }

    public void StartRace()
    {
        startTime = Time.time;
        raceActive = true;
        penaltyTime = 0;
        Debug.Log("Race Started!");
    }

    public void AddPenaltyTime(float time)
    {
        penaltyTime += time;
        Debug.Log("Penalty Added: " + time + " seconds");
    }

    public void EndRace()
    {
        if (raceActive)
        {
            float finalTime = Time.time - startTime + penaltyTime;
            raceActive = false;
            Debug.Log("Race Ended! Final Time: " + finalTime + " seconds");
        }
    }
}
