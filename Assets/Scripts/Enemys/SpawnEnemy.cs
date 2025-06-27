
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    private EnemyConfig config; 
    private void Awake()
    {
        config = new EnemyConfig();
        spawnPoints.AddRange(GetComponentsInChildren<Transform>()); 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            CreateEnemy(new FireEnemyFactory());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            CreateEnemy(new FreezeEnemyFactory());
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            CreateEnemy(new ElectricEnemyFactory());
        }
    }
    private void CreateEnemy(IEnemyFactory enemyFactory)
    {
        int randomIndex = Random.Range(1, spawnPoints.Count);
        Enemy enemy = enemyFactory.CrateEnemy(config,spawnPoints[randomIndex].transform.position);
        enemy.EnemyInit();
    }
}
     