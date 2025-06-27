 
using UnityEngine;

[CreateAssetMenu(fileName = "weaponTrans", menuName = "Wepons")]
public abstract class WeaponData : ScriptableObject
{
    public string weaponName = "animation name";
    public float damage = 0f;
    public AudioClip audioFire;
    public ParticleSystem effectFire;
    public Sprite iconWeapon; 
}
