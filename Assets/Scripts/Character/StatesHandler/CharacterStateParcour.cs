 
using State.Character;
using System;

public class CharacterStateParcour 
{
    public bool isStartParkour { get; private set; }
    public bool isParkour { get; private set; }
    public bool isRayHitToObstacle { get; private set; }
    
    public void SetStateHitToObstacle(bool isHit)
    {
        isRayHitToObstacle = isHit;
    }
    public void SetStartParkour(bool isStartParkour)
    {
        this.isStartParkour = isStartParkour;
    }
    public void SetStateParkour(bool isStateParkour)
    {
        this.isParkour = isStateParkour;
    }
    public void InputCharacter_OnParkour(InputEventJump jumpEvent)
    {
        //OnParcoure?.Invoke();
    } 
}
