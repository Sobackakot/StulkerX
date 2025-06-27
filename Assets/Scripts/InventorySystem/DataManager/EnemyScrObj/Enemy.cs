 
using UnityEngine;
 
public abstract class Enemy : ScriptableObject
{
    public EnemyType enemyType;
    public string nameEnemy;
    public float healthEnemy = 100f;
    public float damageEnemy = 25f;
    public float speedMove = 5f;
    public abstract void EnemyInit();
}
public enum EnemyType
{
    None,
    Fire,
    Freeze,
    Electric
}
