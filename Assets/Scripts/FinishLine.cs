using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameTimer gameTimer; // Assign this in the inspector
    public ScoreManager ScoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameTimer.StopTimer();
            ScoreManager.AddNewScore(gameTimer.finalScore);
        }
    }
}

