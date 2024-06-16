using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioManagerScript : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isMuted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        GameObject.Find("MuteButton").GetComponentInChildren<UnityEngine.UI.Text>().text = isMuted ? "Play" : "Mute";
    }
}
