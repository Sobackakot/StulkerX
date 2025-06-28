
using UnityEngine;
using Zenject;
using Character.InputEvents;

public class StateMachineAnimator : StateMachineBehaviour
{  
    public bool isJump { get; private set; }
    public bool isKinematic;

    public bool isStartClimbing;
    public bool isNextClimbing;
    public bool isFinishClimbing;

    public bool isJumpOn;  
    public bool isStepUp; 
    public bool isJumpingObstacle;

    public bool isParcoureState;

    private IInputEvents inputEvent;

    [Inject]
    private void Construct(IInputEvents inputEvent)
    {
        this.inputEvent = inputEvent;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParkourStateEnter(stateInfo, true);
        JumpStateEnter(stateInfo, true);
        if(layerIndex == 1)
        {
            EquipWeaponEnter(stateInfo, true);
            UnequipWeaponEnter(stateInfo, true);
        } 
        if (layerIndex == 4)
            ReloadWeaponEnter(stateInfo, true); 
    }
  
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        animator.SetBool("isJumping", false);
        ParkourStateEnter(stateInfo, false);
        JumpStateEnter(stateInfo, false);
        if (layerIndex == 1)
        {
            EquipWeaponEnter(stateInfo, false);
            UnequipWeaponEnter(stateInfo, false);
        } 
        if (layerIndex == 4)
            ReloadWeaponEnter(stateInfo, false); 
    }
 
    private void ParkourStateEnter(AnimatorStateInfo stateInfo,bool isStart)
    {
        isStartClimbing = stateInfo.IsName("StartClimbing");
        isNextClimbing = stateInfo.IsName("NextClimbing");
        isFinishClimbing = stateInfo.IsName("FinishClimbing");

        isStepUp = stateInfo.IsName("StepUp");
        isJumpingObstacle = stateInfo.IsName("JumpingObstacle");
        isJumpOn = stateInfo.IsName("JumpOn"); 

        if (isStartClimbing || isStepUp || isJumpOn || isJumpingObstacle)
        {
            isKinematic = isStart;
            isParcoureState = isStart; 
        } 
    }
 
    private void JumpStateEnter(AnimatorStateInfo stateInfo, bool isStart)
    {
        if(stateInfo.IsName("Jump_Run"))
            isJump = isStart;
    }
    private void EquipWeaponEnter(AnimatorStateInfo stateInfo, bool isStart)
    {
        if (stateInfo.IsName("EquipWeapon"))
            inputEvent.SetEquippWeaponAnimationState(isStart);
    }
    private void UnequipWeaponEnter(AnimatorStateInfo stateInfo, bool isStart)
    {
        if (stateInfo.IsName("UnequipWeapon"))
            inputEvent.SetEquippWeaponAnimationState(isStart);
    }
    private void ReloadWeaponEnter(AnimatorStateInfo stateInfo,bool isStart)
    {
        if (stateInfo.IsName("Reloading_Stop"))
            inputEvent.SetReloadWeaponAnimationState(isStart);
    }
   
}
