using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] [Range(0.1f,30f)] float spawnTimer = 1f;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();    
    }

    void Start()
    {
        StartCoroutine(EnemySpawn());    
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0;i < pool.Length; i++)
        {
            pool[i] = Instantiate(Enemy, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
        
    }

    void EnableObjectInPool()
    {
        /*foreach(GameObject swim in pool)
        {
            if (!swim.activeSelf)
            {
                swim.SetActive(true);
                return;
            }
        }*/

        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
