 
using UnityEngine;
 
public class ElectricEnemyFactory : IEnemyFactory
{
    public Enemy CrateEnemy(EnemyConfig conf,Vector3 point)
    {
        var prefab = Resources.Load<GameObject>("Prefabs/Enemys/EnemyElectric");
        GameObject enemyObj = GameObject.Instantiate(prefab, point, Quaternion.identity);
        EnemyGame enemyGame = enemyObj.transform.GetComponent<EnemyGame>();
        conf.AddEnemy(enemyGame);
        return enemyGame.enemyData;
    }
}
