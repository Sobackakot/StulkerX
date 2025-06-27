 
using UnityEngine; 
public interface IEnemyFactory
{  
    Enemy CrateEnemy(EnemyConfig conf, Vector3 point);  
}

