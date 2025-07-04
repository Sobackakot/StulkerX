using State.Character.Move;
using UnityEngine;

namespace Character.Context
{
    public interface IContextStates  
    {
        MoveStateType MoveStateType { get; }
        Vector3 InputAxis { get; }
        Vector2 InputAxisCamera { get; }
        float CurrentAngle { get; }
        bool IsFirstCamera { get; }  
        bool IsIdle { get;}
        bool IsCrouch { get;}
        bool IsWalk { get;} 
        bool IsRun { get; }
        bool IsSprint { get; }

        bool IsHasWeapon { get; }
        bool IsAim { get; }
        bool IsReloadingState { get; }
        bool IsReadyForBattle { get; }
        bool IsEquippingState { get; }

        bool IsRayHitToItem { get; }
        bool IsActiveInventory { get; }
        bool IsRayHitToInventoryLootBox { get; }

        bool IsCollision { get; }
        bool IsLeanRight { get; }
        bool IsLeanLeft { get; }
        bool IsLeftTargerPoint { get; }
        bool IsParkour { get; }
        bool IsFire { get; }

        bool IsRayHitToObstacle { get; } 
    }

}
