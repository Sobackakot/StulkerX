using State.Character; 
using UnityEngine;
using Zenject;

public class WeaponFreeParent : MonoBehaviour
{ 
    private Transform newParent;
    [SerializeField] private Transform weaponOriginParent;
    private Weapon weapon;
    private CharacterInputEventHandler inputEvent;
    [Inject]
    private void Construct(CharacterInputEventHandler inputEvent)
    {
        this.inputEvent = inputEvent; 
    }
    private void Awake()
    {
        newParent = GetComponent<Transform>();
    }
    private void OnEnable()
    {
        inputEvent.OnSetParentWeapon += SetParentWeapon;
        inputEvent.OnResetParenWeapon += ResetParenWeapon;
    }
    private void OnDisable()
    {
        inputEvent.OnSetParentWeapon -= SetParentWeapon;
        inputEvent.OnResetParenWeapon -= ResetParenWeapon;
    }
    private void SetParentWeapon()
    { 
        weapon = weaponOriginParent.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {  
            weapon.transform.SetParent(newParent);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    } 
    private void ResetParenWeapon()
    { 
        if (weapon != null)
        { 
            weapon.transform.SetParent(weaponOriginParent);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }
}
