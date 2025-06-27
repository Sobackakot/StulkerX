using State.Character;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class CharacterParcure : MonoBehaviour
{ 
    private Rigidbody rb;
    private Transform charTrans;
    private RaycastCamera ray; 
    private Animator animator;
    private StateMachineAnimator stateMachin;
    [SerializeField] private List<ObstacleData>  obstaclesData = new List<ObstacleData>();
    private ObstacleData curObst;

    public AnimatorStateInfo animState; 
    
    public bool isStartingParkour {  get; private set; }

    private CharacterStateBootstrap state;

 
    //public void OnEnable()
    //{
    //    state.Parkour.OnParcoure += CharacterState_OnParcoure;
    //}
    //public void OnDisable()
    //{
    //    state.Parkour.OnParcoure -= CharacterState_OnParcoure;
    //}

    //public void CharacterState_OnParcoure()
    //{  
    //     state.Parkour.SetStartParkour(ray.SetRayHitParcour(out RaycastHit hitForward, out RaycastHit hitDown));
         
    //    if (state.Parkour.isStartParkour)
    //    { 
    //        foreach (ObstacleData data in obstaclesData)
    //        {
                
    //            if(data.CheckHeightObstacle(hitForward,hitDown, charTrans))
    //            {
    //                curObst = data; 
    //                onAnimParkour?.Invoke(data.nameStateAnim);
    //                break;
    //            }
    //        }  
    //    } 
    //}
    //public bool UpdateParcour()
    //{
    //    rb.isKinematic = stateMachin.isKinematic;
    //    if (stateMachin.isParcoureState)
    //    { 
    //        animState = animator.GetCurrentAnimatorStateInfo(0);
    //        if (animator.isMatchingTarget || animator.IsInTransition(0) || animState.normalizedTime > curObst.TargetTime)
    //            return false; 
    //        animator.SetTarget(curObst.MatchBody, curObst.TargetTime);
    //        animator.MatchTarget(curObst.matchPoint, charTrans.rotation, curObst.MatchBody,
    //            new MatchTargetWeightMask(curObst.MatchPosWeight, 1), curObst.StartTime, curObst.TargetTime);
    //        charTrans.rotation = Quaternion.RotateTowards(charTrans.rotation, curObst.targetRotate, 360f * Time.deltaTime);
    //        return true;
    //    }
    //    else
    //    {
    //        state.Parkour.SetStartParkour(stateMachin.isParcoureState);
    //        return false;
    //    }
    //}  
}
