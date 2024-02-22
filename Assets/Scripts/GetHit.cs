using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHit : MonoBehaviour
{
    [Tooltip("Determines when the player is taking damage.")]
    public bool hurt = false;

    private bool slipping = false;
    private PlayerMovement playerMovementScript;
    private Rigidbody rb;
    private Transform enemy;
    
    public int maxHealth = 100;
    public int currentHealth; // Changed to public for access from other scripts
    public Slider healthBar;
    
    private bool gameOver = false;
    public Button restartButton; // Reference to the restart button in the UI
    
    private int coinsCollected = 0; // Variable to keep track of coins collected
    
    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        UpdateHealthBar();

        // Disable the restart button initially
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }
    }
    
    // No changes in FixedUpdate

    private void OnCollisionStay(Collision other)
    {
        if (!hurt && !gameOver)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Trap")
            {
                enemy = other.gameObject.transform;
                rb.AddForce(enemy.forward * 1000);
                rb.AddForce(transform.up * 500);
                TakeDamage();
            }
        }

        slipping = other.gameObject.layer == 9;

        if (!slipping)
        {
            playerMovementScript.playerStats.canMove = true;
        }
    }


    private void TakeDamage()
    {
        if (currentHealth > 0 && !gameOver)
        {
            currentHealth -= 10;
            UpdateHealthBar();

            hurt = true;
            playerMovementScript.playerStats.canMove = false;
            playerMovementScript.soundManager.PlayHitSound();
            StartCoroutine("Recover");

            if (currentHealth <= 0)
            {
                gameOver = true;
                if (restartButton != null)
                {
                    restartButton.gameObject.SetActive(true); // Enable restart button on game over
                }
            }
        }
    }

    // Method to add health
    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed maximum
        UpdateHealthBar();
    }

    public void AddCoins(int amount)
    {
        coinsCollected++; // Increment the coins collected counter

        // If player has collected 10 coins, add 10 health and reset the counter
        if (coinsCollected >= 10)
        {
            AddHealth(10);
            coinsCollected = 0; // Reset the counter

            // Update the health bar UI
            UpdateHealthBar();
        }
    }
    
    
    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.75f);
        hurt = false;
        playerMovementScript.playerStats.canMove = true;
    }
    
    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
            healthBar.maxValue = maxHealth;
        }
    }
    
    // Method to restart the game, called when restart button is clicked
    public void RestartGame()
    {
        // Restart the game logic here
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
