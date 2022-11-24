using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    GameObject LM;
    LevelManager lvlMan;
    float spawnDelay;
    bool spawnUpdate = true;

    void Start()
    {
        LM = GameObject.FindGameObjectWithTag("Level Manager");
        lvlMan = LM.GetComponent<LevelManager>();
        spawnDelay = lvlMan.enemySpawnDelay;
    }

    void Update()
    {
        if (lvlMan.numberOfEnemies.Count < lvlMan.maxEnenmies && spawnUpdate == true)
        {
            StartCoroutine(EnemySpawnDelay());
        }
    }

    IEnumerator EnemySpawnDelay()
    {
        spawnUpdate = false;
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(enemy, transform.position, transform.rotation);
        spawnUpdate = true;
    }
}
