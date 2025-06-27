 
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricEnemy", menuName = "Enemy/Electric")]
public class ElectricEnemy : Enemy
{
    public override void EnemyInit()
    {
        enemyType = EnemyType.Electric;
        Debug.Log(enemyType);
    }
}
