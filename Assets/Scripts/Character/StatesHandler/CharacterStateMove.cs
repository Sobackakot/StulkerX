
using State.Character;
using System;
using UnityEngine;


public class CharacterStateMove 
{
    public float speedSprint { get; private set; } = 5f;
    public float speedRunForward { get; private set; } = 3f;
    public float speedRunBack { get; private set; } = 2f;
    public float speedWalkForward { get; private set; } = 1.5f;
    public float speedWalkBack { get; private set; } = 0.5f;
    public float jumpForce { get; private set; } = 3f;
    public float speedRotate { get; private set; } = 45f;
     
    public Transform targetAim { get; private set; }
    public Rigidbody rbChar { get; private set; }  
    public Vector3 inputAxis { get; private set; }
    public Vector3 newDirection { get; set; }
    public Vector3 directionLoock { get; set; }

    public bool isIdle { get; private set; }
    public bool isWalk { get; private set; }
    public bool isRun { get; private set; }
    public bool isSprint { get; private set; }
    
     
    public bool isCollision { get; private set; }
    public bool isMove { get; private set; }
    public bool isLeanRight { get; private set; }
    public bool isLeanLeft { get; private set; }
    public bool isCrouch { get; private set; }
    public bool isLeftTargerPoint { get; private set; }

    public void TiltBody()
    {
        float targetX =0;
        if (isLeanRight) targetX = 1;
        else if (isLeanLeft) targetX = -1f;

        Quaternion rotate = Quaternion.Euler(targetX, 90, 0); 
        targetAim.localRotation  = Quaternion.Slerp(targetAim.localRotation, rotate, speedRotate * Time.fixedDeltaTime);
    }
    
 
 
 
    
    public void InputCharacter_OnJamp(InputEventJump jumpEvent)
    {   
        //if (isCollision && !stateGame.Weapon.isAim)
        //{
        //    if (!stateGame.Parkour.isRayHitToObstacle)
        //        OnJumping?.Invoke(); 
        //}
    }
    public void InputCharacter_OnLeanRight(InputEventLeanRight leanEvent)
    {
        isLeanRight = leanEvent.inputValue;
        isLeftTargerPoint = false;
    }
    public void InputCharacter_OnLeanLeft(InputEventLeanLeft leanEvent)
    {
        isLeanLeft = leanEvent.inputValue;
        isLeftTargerPoint = true;
    }
    public void InputCharacter_OnCrouch(ToggleEventCrouch crouchEvent)
    {
        isCrouch = !isCrouch; 
    }
   
     
    public void SetCollision(bool isCollision)
    {
        this.isCollision = isCollision;
    } 
    public void SetStateMove(bool isMoving)
    {
        this.isMove = isMoving; 
    }
}
