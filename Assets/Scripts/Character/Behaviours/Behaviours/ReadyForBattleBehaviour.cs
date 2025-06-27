using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using UnityEngine;
public class ReadyForBattleBehaviour : BehaviourCharBase
{
    public ReadyForBattleBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        CharacterInputEventHandler inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IReadyForBattleBehaviour>(this);
    }
    public override void EnableBeh()
    { 
        ReadyForBattle(stateData.isReadyForBattle);
    }
    public override void DisableBeh()
    {
        ReadyForBattle(stateData.isReadyForBattle);
    }
    public override void UpdateBeh()
    { 
    }
    public override void LateUpdateBeh()
    {
    }
    public override void FixedUpdateBeh()
    { 
    }
    public override void ReadyForBattle(bool isReady)
    { 
        animator.ReadyForBattleAnim(isReady);
    }
}
