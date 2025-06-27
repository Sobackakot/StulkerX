
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEnemy", menuName = "Enemy/Freeze")]
public class FreezeEnemy : Enemy
{
    public override void EnemyInit()
    { 
        enemyType = EnemyType.Freeze;
        Debug.Log(enemyType);
    }
}
