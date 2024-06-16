using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ObstacleCollision : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            impulseSource.GenerateImpulse();
        }
    }
}