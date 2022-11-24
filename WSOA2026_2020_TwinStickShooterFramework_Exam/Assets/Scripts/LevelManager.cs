using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public List<GameObject> numberOfEnemies = new List<GameObject>();
    public int maxEnenmies = 10;
    public float enemySpawnDelay = 2;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
