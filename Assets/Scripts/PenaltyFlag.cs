using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyFlag : MonoBehaviour
{
    public enum Side { Left, Right }
    public Side side;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((side == Side.Left && !PlayerOnCorrectSide(other.transform.position.x, transform.position.x)) ||
                (side == Side.Right && PlayerOnCorrectSide(other.transform.position.x, transform.position.x)))
            {
                RaceManager.Instance.AddPenaltyTime(1);
            }
        }
    }

    private bool PlayerOnCorrectSide(float playerX, float flagX)
    {
        // Assuming player must be to the left of the flag for 'Left' and right for 'Right'
        return (side == Side.Left) ? playerX < flagX : playerX > flagX;
    }
}