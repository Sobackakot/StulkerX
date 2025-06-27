
using System; 
using UnityEngine; 
using Zenject;

public class InputCharacter : IInitializable, IDisposable
{   
    private InputActions inputActions;

    public void Initialize()
    { 
        inputActions = new InputActions();
        inputActions.Enable();
         
        inputActions.ActionMaps.Jump.performed += ctx => EventBus.Publish(new InputEventJump());
        inputActions.ActionMaps.ReloadWeapon.performed += ctx => EventBus.Publish(new ReloadWeaponEvent());  

        inputActions.ActionMaps.ExitInventory.performed += ctx => EventBus.Publish(new InventoryExitEvent());
        inputActions.ActionMaps.ToggleInventory.performed += ctx => EventBus.Publish(new InventoryActiveEvent()); 
        inputActions.ActionMaps.ToggleInventoryBox.performed += ctx => EventBus.Publish(new InventoryLootBoxActiveEvent());
        inputActions.ActionMaps.PickUpItem.performed += ctx => EventBus.Publish(new PickUpItemEvent());
        inputActions.ActionMaps.ToggleEquipWeapon.performed += ctx => EventBus.Publish(new EquipWeaponToggleEvent());
        inputActions.ActionMaps.Crouch.performed += ctx => EventBus.Publish(new ToggleEventCrouch());

        inputActions.ActionMaps.Move.performed += ctx => EventBus.Publish(new InputEventMove(ctx.ReadValue<Vector2>()));
        inputActions.ActionMaps.Move.canceled += ctx => EventBus.Publish(new InputEventMove(Vector2.zero));

        inputActions.ActionMaps.Run.performed += ctx => EventBus.Publish(new InputEventSprint(true));
        inputActions.ActionMaps.Run.canceled += ctx => EventBus.Publish(new InputEventSprint(false));

        inputActions.ActionMaps.Walk.performed += ctx => EventBus.Publish(new InputEventWalk(true));
        inputActions.ActionMaps.Walk.canceled += ctx => EventBus.Publish(new InputEventWalk(false));

        

        inputActions.ActionMaps.Aim.started += ctx => EventBus.Publish(new AimInputEvent(true));
        inputActions.ActionMaps.Aim.canceled += ctx => EventBus.Publish(new AimInputEvent(false));

        inputActions.ActionMaps.Fire.started += ctx => EventBus.Publish(new FireInputEvent(true));
        inputActions.ActionMaps.Fire.canceled += ctx => EventBus.Publish(new FireInputEvent(false));

        inputActions.ActionMaps.TiltRight.started += ctx => EventBus.Publish(new InputEventLeanRight(true));
        inputActions.ActionMaps.TiltRight.canceled += ctx => EventBus.Publish(new InputEventLeanRight(false));

        inputActions.ActionMaps.TiltLeft.started += ctx => EventBus.Publish(new InputEventLeanLeft(true));
        inputActions.ActionMaps.TiltLeft.canceled += ctx => EventBus.Publish(new InputEventLeanLeft(false));

    }

    public void Dispose()
    {
        inputActions.ActionMaps.Jump.performed -= ctx => EventBus.Publish(new InputEventJump());
        inputActions.ActionMaps.ReloadWeapon.performed -= ctx => EventBus.Publish(new ReloadWeaponEvent());  

        inputActions.ActionMaps.ExitInventory.performed -= ctx => EventBus.Publish(new InventoryExitEvent());
        inputActions.ActionMaps.ToggleInventory.performed -= ctx => EventBus.Publish(new InventoryActiveEvent());
        inputActions.ActionMaps.ToggleInventoryBox.performed -= ctx => EventBus.Publish(new InventoryLootBoxActiveEvent());
        inputActions.ActionMaps.PickUpItem.performed -= ctx => EventBus.Publish(new PickUpItemEvent());
        inputActions.ActionMaps.ToggleEquipWeapon.performed -= ctx => EventBus.Publish(new EquipWeaponToggleEvent());
        inputActions.ActionMaps.Crouch.performed -= ctx => EventBus.Publish(new ToggleEventCrouch());

        inputActions.ActionMaps.Move.performed -= ctx => EventBus.Publish(new InputEventMove(ctx.ReadValue<Vector2>()));
        inputActions.ActionMaps.Move.canceled -= ctx => EventBus.Publish(new InputEventMove(Vector2.zero));

        inputActions.ActionMaps.Run.performed -= ctx => EventBus.Publish(new InputEventSprint(true));
        inputActions.ActionMaps.Run.canceled -= ctx => EventBus.Publish(new InputEventSprint(false));

        inputActions.ActionMaps.Walk.performed -= ctx => EventBus.Publish(new InputEventWalk(true));
        inputActions.ActionMaps.Walk.canceled -= ctx => EventBus.Publish(new InputEventWalk(false));

        

        inputActions.ActionMaps.Aim.started -= ctx => EventBus.Publish(new AimInputEvent(true));
        inputActions.ActionMaps.Aim.canceled -= ctx => EventBus.Publish(new AimInputEvent(false));

        inputActions.ActionMaps.Fire.started -= ctx => EventBus.Publish(new FireInputEvent(true));
        inputActions.ActionMaps.Fire.canceled -= ctx => EventBus.Publish(new FireInputEvent(false));

        inputActions.ActionMaps.TiltRight.started -= ctx => EventBus.Publish(new InputEventLeanRight(true));
        inputActions.ActionMaps.TiltRight.canceled -= ctx => EventBus.Publish(new InputEventLeanRight(false));

        inputActions.ActionMaps.TiltLeft.started -= ctx => EventBus.Publish(new InputEventLeanLeft(true));
        inputActions.ActionMaps.TiltLeft.canceled -= ctx => EventBus.Publish(new InputEventLeanLeft(false));
        inputActions.Dispose(); 
    }
}
 
public struct InputEventMove 
{
    public InputEventMove(Vector2 direction) => inputValue = direction;
    public Vector2 inputValue { get; set; }   
   
} 

public struct InputEventJump { } 

public struct InputEventSprint
{
    public InputEventSprint(bool isRunning) => inputValue = isRunning;
    public bool inputValue { get; set; } 
}  

public struct InputEventWalk
{
    public InputEventWalk(bool isWalking) => inputValue = isWalking;
    public bool inputValue { get; set; } 
}  

public struct ReloadWeaponEvent { }  

public struct AimInputEvent
{
    public AimInputEvent(bool isAiming) => inputValue = isAiming;
    public bool inputValue { get; set; } 
}  

public struct FireInputEvent
{
    public FireInputEvent(bool isFiring) => inputValue = isFiring;
    public bool inputValue { get; set; } 
}  

public struct EquipWeaponToggleEvent { } 

public struct InputEventLeanRight
{
    public InputEventLeanRight(bool isLeaningRight) => inputValue = isLeaningRight;
    public bool inputValue { get; set; } 
}    

public struct InputEventLeanLeft
{
    public InputEventLeanLeft(bool isLeaningLeft) => inputValue = isLeaningLeft;
    public bool inputValue { get; set; } 
}  
  
public struct ToggleEventCrouch 
{ 
}  

public struct InventoryExitEvent { } 
public struct InventoryActiveEvent
{
    public bool inputValue { get; set; }
} 

public struct InventoryLootBoxActiveEvent
{
    public bool inputValue { get; set; }
} 

public struct PickUpItemEvent 
{
    public bool inputValue { get; set; }
}  