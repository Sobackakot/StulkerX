using Character.InputEvents;
using UnityEngine;
using Zenject;

public class WeaponFreeParent : MonoBehaviour
{ 
    private Transform newParent;
    private Transform weaponOriginParent;
    private Transform trWeapon;
    private IInputEvents inputEvent;
    [Inject]
    private void Construct(IInputEvents inputEvent)
    {
        this.inputEvent = inputEvent; 
    }
    private void Awake()
    {
        newParent = GetComponent<Transform>();
        weaponOriginParent = FindObjectOfType<WeaponHandle>()?.transform;
    }
    private void OnEnable()
    { 
        if(inputEvent != null)
        {
            inputEvent.OnSetParentWeapon += SetParentWeapon;
            inputEvent.OnResetParenWeapon += ResetParenWeapon;
        } 
    }
    private void OnDisable()
    {
        if (inputEvent != null)
        {
            inputEvent.OnSetParentWeapon -= SetParentWeapon;
            inputEvent.OnResetParenWeapon -= ResetParenWeapon;
        }
    }
    private void SetParentWeapon()
    {
        trWeapon = weaponOriginParent.GetComponentInChildren<Weapon>()?.transform;
        if (trWeapon != null)
        {
            trWeapon.SetParent(newParent);
            trWeapon.localPosition = Vector3.zero;
            trWeapon.localRotation = Quaternion.identity;
        }
    } 
    private void ResetParenWeapon()
    { 
        if (trWeapon != null)
        {
            trWeapon.SetParent(weaponOriginParent);
            trWeapon.localPosition = Vector3.zero;
            trWeapon.localRotation = Quaternion.identity;
        }
    }
}
