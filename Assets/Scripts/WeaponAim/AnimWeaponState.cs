 
using UnityEngine;
using Zenject;
using State.Character;

public class AnimWeaponState : StateMachineBehaviour
{
    private CharacterStateBootstrap handlerState;

    [Inject]
    private void Construct(CharacterStateBootstrap handlerState)
    {
        this.handlerState = handlerState;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  
    }

    private void ReloadWeaponStateEnter(AnimatorStateInfo stateInfo)
    { 
        if (stateInfo.IsName("WeaponReload"))
        {
            //stateChar.SetReloadWeaponAnimationState(true); 
        }

    }
    private void ReloadWeaponStateExit(AnimatorStateInfo stateInfo)
    { 
        if (stateInfo.IsName("WeaponReload"))
        {
            //stateChar.SetReloadWeaponAnimationState(false); 
        }
    }
}
