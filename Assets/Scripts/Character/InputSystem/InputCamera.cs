
using System; 
using UnityEngine; 
using Zenject;

public class InputCamera : IInitializable, IDisposable
{    
   
    private InputActions inputActions; 
    public void Initialize()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.ActionMaps.CameraSwitch.performed += ctx => EventBus.Publish(new SwitchEventCamera());
        inputActions.ActionMaps.MouseDelta.performed += ctx => EventBus.Publish(new InputEventCamera(ctx.ReadValue<Vector2>()));
        inputActions.ActionMaps.MouseScroll.performed += ctx => EventBus.Publish(new CameraZoomEvent(ctx.ReadValue<Vector2>()));
    } 
    public void Dispose()
    {
        inputActions.ActionMaps.CameraSwitch.performed -= ctx => EventBus.Publish(new SwitchEventCamera());
        inputActions.ActionMaps.MouseDelta.performed -= ctx => EventBus.Publish(new InputEventCamera(Vector2.zero));
        inputActions.ActionMaps.MouseScroll.performed -= ctx => EventBus.Publish(new CameraZoomEvent(Vector2.zero));

        inputActions.Disable();
    } 
}
public struct SwitchEventCamera
{
    public bool IsFirstPerson; 
}
public struct InputEventCamera
{
    public Vector2 InputAxis;
    public InputEventCamera(Vector2 inputAxis) => InputAxis = inputAxis;
}
public struct CameraZoomEvent
{
    public Vector2 ScrollZoom;
    public CameraZoomEvent(Vector2 scrollZoom) => ScrollZoom = scrollZoom;
}