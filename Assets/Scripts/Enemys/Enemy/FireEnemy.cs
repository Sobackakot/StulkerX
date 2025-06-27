 
using UnityEngine;

[CreateAssetMenu(fileName = "FireEnemy", menuName = "Enemy/Fire")]
public class FireEnemy : Enemy
{
    public override void EnemyInit()
    { 
        enemyType = EnemyType.Fire;
        Debug.Log(enemyType);
    }
}
