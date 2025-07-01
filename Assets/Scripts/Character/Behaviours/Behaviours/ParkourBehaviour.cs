using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using Character.InputEvents;
using StateData.Character;
using System;
using UnityEngine;
using MainCamera.Raycast;
public class ParkourBehaviour : BehaviourCharBase
{
    public ParkourBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<IParkourBehaviour>(this);
    }
    public event Action<string> onAnimParkour;  
    private ObstacleData curObst; 
    public AnimatorStateInfo animState; 

    bool isStartParkour;
    public override void EnableBeh()
    { 
        inputEvent.OnParkour += ParkouringBehaviour;
    }
    public override void DisableBeh()
    {
        inputEvent.OnParkour -= ParkouringBehaviour;
    }
    public override void UpdateBeh()
    { 
    }
    public override void LateUpdateBeh()
    {
    }
    public override void FixedUpdateBeh()
    {
        isStartParkour = UpdateParcour();
    } 
    public override void ParkouringBehaviour()
    {
        isStartParkour = character.raycastHitParcour.SetRayHitParcour(out RaycastHit hitForward, out RaycastHit hitDown);

        if (isStartParkour)
        {
            foreach (ObstacleData data in character.obstaclesData)
            {

                if (data.CheckHeightObstacle(hitForward, hitDown, character.charTrans))
                {
                    curObst = data;
                    onAnimParkour?.Invoke(data.nameStateAnim);
                    break;
                }
            }
        }
    } 
    public bool UpdateParcour()
    {
        character.rbCharacter.isKinematic = character.stateMachin.isKinematic;
        if (character.stateMachin.isParcoureState)
        {
            animState = character.animator.GetCurrentAnimatorStateInfo(0);
            if (character.animator.isMatchingTarget || character.animator.IsInTransition(0) || animState.normalizedTime > curObst.TargetTime)
                return false;
            character.animator.SetTarget(curObst.MatchBody, curObst.TargetTime);
            character.animator.MatchTarget(curObst.matchPoint, character.charTrans.rotation, curObst.MatchBody,
                new MatchTargetWeightMask(curObst.MatchPosWeight, 1), curObst.StartTime, curObst.TargetTime);
            character.charTrans.rotation = Quaternion.RotateTowards(character.charTrans.rotation, curObst.targetRotate, 360f * Time.deltaTime);
            return true;
        }
        else
        {
            isStartParkour = false;
            return false;
        }
    }
}
