using State.Behaviour;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMoveAnim : NPCStateBace
{
    public NPCStateMoveAnim(NPCStateBootstrap stateNPC) : base(stateNPC)
    {
    }

    private Animator animNPC;
    private NavMeshAgent agent;

    public float switchAngleTurn { get; private set; }
    public float speedWalk { get; private set; }
    public float speedRun { get; private set; }
    public float speedSprint { get; private set; }

    public int runningLayer { get; private set; }
    public int reloadWeaponLayer { get; private set; }
    public int equipWeaponLayer { get; private set; }


    public void SetAnimator(Animator animNPC)
    {
        this.animNPC = animNPC;
    }
    public void MoveAnimation()
    {
        if (animNPC.velocity.sqrMagnitude > 0.2f)
        {
            animNPC.SetFloat("X", SpeedCalculate(), 0.2f, Time.smoothDeltaTime);
            animNPC.SetFloat("Y", SpeedCalculate(), 0.2f, Time.smoothDeltaTime);
        }
        else
        {
            animNPC.SetFloat("Y", 0, 0.2f, Time.smoothDeltaTime);
            animNPC.SetFloat("X", 0, 0.2f, Time.smoothDeltaTime);
        } 
    }
    private float SpeedCalculate()
    {
        return agent.velocity.magnitude / agent.speed;
    }
   
    public void CrouchAnimation(bool isCrouching)
    {
        animNPC.SetBool("isCrouching", isCrouching);
    }
    public void StartingRunning()
    {
        runningLayer = animNPC.GetLayerIndex("Running_Layer");
        animNPC.SetLayerWeight(runningLayer, 1);
        animNPC.SetTrigger("StartingRun");
    }
    public void StoppingRunning()
    {
        runningLayer = animNPC.GetLayerIndex("Running_Layer");
        animNPC.SetLayerWeight(runningLayer, 1);
        animNPC.SetTrigger("StoppingRun");
    }
    public void ReadyForBattleAnim(bool isReadyForBattle)
    {
        animNPC.SetBool("isReadyForBattle", isReadyForBattle);
    }
    public void AimingAnimation(bool isAiming)
    {
        animNPC.SetBool("isAiming", isAiming);
    }
    public void CharacterState_OnWeaponEquip(bool isEquipWeapon)
    {
        animNPC.SetLayerWeight(equipWeaponLayer, 1);
        if (isEquipWeapon)
            animNPC.SetTrigger("EquipWeapon");
        else animNPC.SetTrigger("UnquipWeapon");
    }
    public void TurnAnimation(Vector3 input, bool isRotate, bool isLimitAngle)
    { 
        if (isRotate && isLimitAngle && Mathf.Abs(input.x) > 0.1f)
        {
            float currentDeltaMouse = animNPC.GetFloat("DeltaMouse");
            float smoothDeltaMouse = Mathf.Lerp(currentDeltaMouse, input.x * switchAngleTurn, 0.1f);
            animNPC.SetFloat("DeltaMouse", smoothDeltaMouse, 0.1f, Time.smoothDeltaTime);
        }
        else animNPC.SetFloat("DeltaMouse", 0, 0.1f, Time.smoothDeltaTime);
    }
}
