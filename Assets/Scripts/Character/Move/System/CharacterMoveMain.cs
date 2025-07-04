using Behaviour.Handler;
using Character.Context;
using Character.InputEvents;
using State.Character.Battle;
using State.Character.Move;
using State.Character.Weapon;
using Behaviour.Character;
public class CharacterMoveMain  
{
    public CharacterMoveMain(
        
        CharacterInspector charac, 
        CharacterAnimatorInspector animator,
        IBehaviourHandler behaviourHandler, 
        IInputEvents inputEvent,
        IContextEvents contextEvents,
        IContextStates contextStates)
    {
        
        this.characterMove = charac;
        this.characterAnimator = animator;
        this.behaviourHandler = behaviourHandler;
        this.inputEvent = inputEvent;
        this.contextEvents = contextEvents;
        this.contextStates = contextStates;

          
        planer = new();
    } 
    private CharacterInspector characterMove;
    private CharacterAnimatorInspector characterAnimator; 

     
    private IInputEvents inputEvent;
    public IContextEvents contextEvents { get; private set; }
    public IContextStates contextStates { get; private set; }
    private PlanerCharacter<IContextEvents> planer;

    private IBehaviourHandler behaviourHandler; 

    private MoveStateHandler moveFSM;
    private ReadyForBattleStateHandler readyFSM;
    private WeaponStateHandler weaponFSM;
     
    public void Initialize()
    {
        inputEvent.Initialize();
        InitializeBehaviour();
        InitializeMoveState();
        InitializeReadyForBattleState();
        InitializeWeaponState(); 
        characterMove.UpdateDirectionMove();


        InitializeActions();
        planer.OnEnable(contextEvents);
    }

    public void Dispose()
    {
        inputEvent.Dispose();
        planer.OnDisable(contextEvents);
    }
    private void InitializeActions()
    {
        planer.AddAction(new MoveAction(moveFSM)); 
        //planer.AddAction(new ReadyAction(readyFSM)); 
        //planer.AddAction(new WeaponAction(weaponFSM)); 
    }
    private void InitializeBehaviour()
    {
        behaviourHandler.Register<IIdleBehaviour>(new IdleBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IAimWeaponBehaviour>(new AimWeaponBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<ICrouchBehaviour>(new CrouchBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IEquipWeaponBehaviour>(new EquipWeaponBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IFireWeaponBehaviour>(new FireWeaponBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IJumpBehaviour>(new JumpBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<ILeanBehaviour>(new LeanBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IMoveBehaviour>(new MoveBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IParkourBehaviour>(new ParkourBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IPickUpItemBehaviour>(new PickUpItemBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IReadyForBattleBehaviour>(new ReadyForBattleBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IReloadWeaponBehaviour>(new ReloadWeaponBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IRotateBehaviour>(new RotateBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IRunBehaviour>(new RunBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<ISprintBehaviour>(new SprintBehaviour(characterMove, characterAnimator));
        behaviourHandler.Register<IWalkBehaviour>(new WalkBehaviour(characterMove, characterAnimator));
    }
    private void InitializeWeaponState()
    {
        weaponFSM = new WeaponStateHandler();
        weaponFSM.RegisterFSM(WeaponStateType.Aim, new WeaponStateAim(weaponFSM, contextStates, behaviourHandler)); 
        weaponFSM.RegisterFSM(WeaponStateType.Reload, new WeaponStateReload(weaponFSM, contextStates, behaviourHandler));
        weaponFSM.RegisterFSM(WeaponStateType.Default, new WeaponStateDefault(weaponFSM, contextStates, behaviourHandler));
        weaponFSM.SetFSM(WeaponStateType.Default); 
    }
    private void InitializeReadyForBattleState()
    {
        readyFSM = new ReadyForBattleStateHandler(); 
        readyFSM.RegisterFSM(ReadyStateType.Ready, new ReadyForBattleState(readyFSM, contextStates, behaviourHandler));
        readyFSM.RegisterFSM(ReadyStateType.None, new NoneReadyForBattleState(readyFSM, contextStates, behaviourHandler));
        readyFSM.SetFSM(ReadyStateType.None); 
    }
    private void InitializeMoveState()
    {
        moveFSM = new MoveStateHandler(); 
        moveFSM.RegisterFSM(MoveStateType.Idle, new MoveStateIdle(moveFSM, contextStates, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Walk, new MoveStateWalk(moveFSM, contextStates, behaviourHandler)); 
        moveFSM.RegisterFSM(MoveStateType.Run, new MoveStateRun(moveFSM, contextStates, behaviourHandler)); 
        moveFSM.RegisterFSM(MoveStateType.Sprint, new MoveStateSprint(moveFSM, contextStates, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Crouch, new MoveStateCrouch(moveFSM, contextStates, behaviourHandler));
        moveFSM.RegisterFSM(MoveStateType.Aim, new MoveStateAim(moveFSM, contextStates, behaviourHandler));
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
        characterMove.UpdateDirectionMove();
        moveFSM?.UpdateFSM();
        readyFSM?.UpdateFSM();
        weaponFSM?.UpdateFSM();
    } 
}
