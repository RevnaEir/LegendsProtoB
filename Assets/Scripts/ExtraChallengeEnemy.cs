using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraChallengeEnemy : MonoBehaviour
{
    
    public Stats enemyStats;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform[] patrolPoints;

    private int currentPatrolPoint = 1; //f ir prieks float vertibam

    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public class Stats
    {
        [Header("Enemy Settings")]

        [Tooltip("How fast the enemy moves.")]
        public float speed =10f;                    //f cuz float

        [Tooltip("Whether the enemy should move or not")]
        public bool move;

    }

    void Update()
    {
        
        //if the enemy is allowed to move
        if (enemyStats.move)
        {
            Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, enemyStats.speed * Time.deltaTime);
           
            if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
            {
                currentPatrolPoint++;
                
                if (currentPatrolPoint >= patrolPoints.Length)  //jasamaina so vins kustas 
                {
                    currentPatrolPoint = 0;
                }
            
            }
        }
        
    }
    
}
