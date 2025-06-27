 
using UnityEngine;

public class WeaponHandle : MonoBehaviour
{
    private Transform trWeapon; 
    private void Awake()
    {
        trWeapon = GetComponent<Transform>();
    }
    public bool SetWeapon(GameObject weapon)
    {
        if (weapon == null) return false;
        else
        {
            weapon.transform.SetParent(trWeapon);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.GetComponent<Rigidbody>().isKinematic = true;
            weapon.transform.GetComponent<Collider>().enabled = false;
            return true;
        }  
    }
   
}
