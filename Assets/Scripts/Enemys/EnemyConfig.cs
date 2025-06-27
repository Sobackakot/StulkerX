
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyConfig  
{
    private List<EnemyGame> enemys = new List<EnemyGame>();

    public void AddEnemy(EnemyGame enemy)
    {
        enemys.Add(enemy);
    }
    public void RemoveEnemy(EnemyGame enemy)
    {
        enemys.Remove(enemy);
    }
    public List<EnemyGame> GetEnemyList()
    {
        return enemys;
    }
}
