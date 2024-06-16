using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().QuickBoost();
            SoundManager.Instance.PlaySoundEffect(SoundManager.SoundEffect.Whoosh);
        }
    }
}
