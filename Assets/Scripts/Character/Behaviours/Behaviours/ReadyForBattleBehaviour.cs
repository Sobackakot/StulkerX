using Behaviour.Character.Base;
public class ReadyForBattleBehaviour : BehaviourCharBase
{
    public ReadyForBattleBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    { 
        ReadyForBattle(character.contextStates.IsReadyForBattle);
    }
    public override void DisableBeh()
    {
        ReadyForBattle(character.contextStates.IsReadyForBattle);
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
