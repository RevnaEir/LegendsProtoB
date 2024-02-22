using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    [Tooltip("The particles that appear after the player collects a coin.")]
    public GameObject coinParticles;

    private GetHit playerHealthScript;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealthScript = other.GetComponent<GetHit>();
            if (playerHealthScript != null)
            {
                print("segfdshds");
                ScoreManager.score += 10;
                playerHealthScript.AddCoins(1);

                GameObject particles = Instantiate(coinParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}