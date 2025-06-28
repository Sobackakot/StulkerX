using Behaviour.Handler;
using Character.InputEvents;
using State.Character.Battle;
using State.Character.Move;
using State.Character.Weapon;
using StateData.Character; 
public class CharacterMoveMain  
{
    public CharacterMoveMain(

        CharacterInspector charac, 
        CharacterAnimatorInspector animator,
        IInputEvents inputEvent,
        CharacterStateContext stateContex)
    {
        behaviourHandler = new();
        this.charac = charac;
        this.animator = animator;
        this.inputEvent = inputEvent;
        this.stateContext = stateContex;
         
        bootstrap = new();
        planer = new();
    } 
    private CharacterInspector charac;
    private CharacterAnimatorInspector animator; 

     
    private IInputEvents inputEvent;
    private CharacterStateContext stateContext;
    private PlanerCharacter<CharacterStateContext> planer;

    private BehaviourHandler behaviourHandler;
    private BehaviourBootstrap bootstrap;

    private MoveStateHandler moveFSM;
    private ReadyForBattleStateHandler readyFSM;
    private WeaponStateHandler weaponFSM;

    public void Initialize()
    { 
        InitializeBehaviour();
        InitializeMoveState();
        InitializeReadyForBattleState();
        InitializeWeaponState(); 
        charac.UpdateDirectionMove();


        InitializeActions();
        planer.OnEnable(stateContext);
    }

    public void Dispose()
    {
        planer.OnDisable(stateContext);
    }
    private void InitializeActions()
    {
        planer.AddAction(new MoveAction(moveFSM)); 
        planer.AddAction(new ReadyAction(readyFSM)); 
        planer.AddAction(new WeaponAction(weaponFSM)); 
    }
    private void InitializeBehaviour()
    {
        bootstrap.InitBehaviourHandler(behaviourHandler);
        bootstrap.InitIdleBeh(new IdleBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitJumpBeh(new JumpBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitRotateBeh(new RotateBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitMoveBeh(new MoveBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler)); 
        bootstrap.InitRunBeh(new RunBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitSprintBeh(new SprintBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitWalkBeh(new WalkBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitCrouchBeh(new CrouchBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitEquipWeaponBeh(new EquipWeaponBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitFireWeaponBeh(new FireWeaponBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitAimBeh(new AimWeaponBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler)); 
        bootstrap.InitPickUpBeh(new PickUpItemBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitLeanBeh(new LeanBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler)); 
        bootstrap.InitParkourBeh(new ParkourBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitReadyBeh(new ReadyForBattleBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        bootstrap.InitReloadWeaponBeh(new ReloadWeaponBehaviour(charac, animator, stateContext, inputEvent, behaviourHandler));
        
    }
    private void InitializeWeaponState()
    {
        weaponFSM = new WeaponStateHandler();
        weaponFSM.RegisterFSM(WeaponStateType.Aim, new WeaponStateAim(weaponFSM, stateContext, behaviourHandler)); 
        weaponFSM.RegisterFSM(WeaponStateType.Reload, new WeaponStateReload(weaponFSM, stateContext, behaviourHandler));
        weaponFSM.RegisterFSM(WeaponStateType.Default, new WeaponStateDefault(weaponFSM, stateContext, behaviourHandler));
        weaponFSM.SetFSM(WeaponStateType.Default); 
    }
    private void InitializeReadyForBattleState()
    {
        readyFSM = new ReadyForBattleStateHandler(); 
        readyFSM.RegisterFSM(ReadyStateType.Ready, new ReadyForBattleState(readyFSM, stateContext, behaviourHandler));
        readyFSM.RegisterFSM(ReadyStateType.None, new NoneReadyForBattleState(readyFSM, stateContext, behaviourHandler));
        readyFSM.SetFSM(ReadyStateType.None); 
    }
    private void InitializeMoveState()
    {
        moveFSM = new MoveStateHandler(); 
        moveFSM.RegisterFSM(MoveStateType.Idle, new MoveStateIdle(moveFSM, stateContext, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Walk, new MoveStateWalk(moveFSM, stateContext, behaviourHandler)); 
        moveFSM.RegisterFSM(MoveStateType.Run, new MoveStateRun(moveFSM, stateContext, behaviourHandler)); 
        moveFSM.RegisterFSM(MoveStateType.Sprint, new MoveStateSprint(moveFSM, stateContext, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Crouch, new MoveStateCrouch(moveFSM, stateContext, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Aim, new MoveStateAim(moveFSM, stateContext, behaviourHandler));
        moveFSM.SetFSM(MoveStateType.Idle); 
    }
    public void FixedTick()  
    {
        moveFSM?.FixedUpdateFSM();
        readyFSM?.FixedUpdateFSM();
        weaponFSM?.FixedUpdateFSM();
    }

    public void LateTick()
    {
        moveFSM?.LateUpdateFSM();
        readyFSM?.LateUpdateFSM();
        weaponFSM?.LateUpdateFSM();
    }

    public void Tick()
    { 
        charac.UpdateDirectionMove();
        moveFSM?.UpdateFSM();
        readyFSM?.UpdateFSM();
        weaponFSM?.UpdateFSM();
    } 
}
