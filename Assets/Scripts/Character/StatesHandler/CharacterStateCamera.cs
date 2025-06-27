using State.Character;
using System;
using UnityEngine;

public class CharacterStateCamera : CharacterStateBase
{
    public CharacterStateCamera(CharacterStateBootstrap stateGameHandler) : base(stateGameHandler)
    {
    }

    public override void EnterState()
    {
        EventBus.Subscribe<SwitchEventCamera>(InputCamera_OnSwitchCamera);
        EventBus.Subscribe<InputEventCamera>(InputCamera_OnInputAxisCamera);

    }

    public override void ExitState()
    {
        EventBus.Unsubscribe<SwitchEventCamera>(InputCamera_OnSwitchCamera);
        EventBus.Unsubscribe<InputEventCamera>(InputCamera_OnInputAxisCamera);
    }
     
    public event Action<Vector2> OnInputAxis;
    public Vector3 inputAxis;
    public float currentAngle { get; private set; }
    public bool isFerst { get; set; }
    public bool isStopingRotate { get; private set; }
    public bool isMaxAngle { get; private set; } 

    public void InputCamera_OnInputAxisCamera(InputEventCamera inputAxis)
    {
        this.inputAxis = new Vector3(inputAxis.InputAxis.x, 0, inputAxis.InputAxis.y);
        OnInputAxis?.Invoke(inputAxis.InputAxis);
    }
    public void SetAngleForCamera(float angle)
    {
        currentAngle = angle;
        if (Mathf.Abs(currentAngle) > 5)
            isMaxAngle = true;
        else isMaxAngle = false;
    }
   
    public void SetStateRotateCamera(bool isActiveInventory)
    { 
        isStopingRotate = isActiveInventory ? false : true;
    }
    public void InputCamera_OnSwitchCamera(SwitchEventCamera a)
    {
        isFerst = !isFerst;
        a.IsFirstPerson = isFerst;
    }
     
}
