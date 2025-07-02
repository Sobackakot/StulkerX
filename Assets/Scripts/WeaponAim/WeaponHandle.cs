 
using UnityEngine;
using MainCamera.Raycast;

public class WeaponHandle : MonoBehaviour
{
    private Transform trWeapon;
    private RaycastPointCamera ray;

    private void Awake()
    {
        trWeapon = GetComponent<Transform>();
        ray = FindObjectOfType<RaycastPointCamera>();
    }

    private void OnEnable()
    {
        if (ray == null) return;
        ray.onSetParentByWeapon += SetParentByWeapon;
    }
    private void OnDisable()
    {
        if (ray == null) return;
        ray.onSetParentByWeapon -= SetParentByWeapon;
    }
    private bool SetParentByWeapon(GameObject weapon)
    {
        if (weapon == null) return false;
        else
        {
            weapon.transform?.SetParent(trWeapon);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.GetComponent<Rigidbody>().isKinematic = true;
            weapon.transform.GetComponent<Collider>().enabled = false;
            return true;
        }  
    }
   
}
