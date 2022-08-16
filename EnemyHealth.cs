using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxhitPoints = 5;
    [Tooltip("Adds amount to max hitpoints when enemy dies.")]
    [SerializeField] int difficultyRank = 1;

    int currentHitPoints = 0;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        currentHitPoints = maxhitPoints;   
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxhitPoints += difficultyRank;
            enemy.RewardGold();
        }
    }
}
