using State.CoreFSM;

namespace State.Character.Weapon
{
    public class WeaponStateHandler : StateMachine<WeaponStateType, IWeaponState>
    { 
    }
    public enum WeaponStateType
    { 
        Aim,
        Reload,
        Default
    }
    public interface IWeaponState : IState
    {
    }
}

