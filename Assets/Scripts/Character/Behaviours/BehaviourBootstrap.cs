using Behaviour.Character;
using Behaviour.Handler;

public class BehaviourBootstrap  
{
    public IBehaviourHandler behaviourHandler { get; private set; }
    public IIdleBehaviour idle { get; private set; }
    public IMoveBehaviour move { get; private set; }        
    public IWalkBehaviour walk { get; private set; }
    public ICrouchBehaviour crouch { get; private set; }
    public IRunBehaviour run { get; private set; }
    public ISprintBehaviour sprint { get; private set; }
    public IRotateBehaviour rotate { get; private set; }
    public ILeanBehaviour lean { get; private set; }
    public IJumpBehaviour jump { get; private set; }
    public IParkourBehaviour parkour { get; private set; }
    public IPickUpItemBehaviour pickUp { get; private set; }
    public IReadyForBattleBehaviour ready { get; private set; }
    public IAimWeaponBehaviour aim { get; private set; }
    public IEquipWeaponBehaviour equip { get; private set; }
    public IReloadWeaponBehaviour reaload { get; private set; }
    public IFireWeaponBehaviour fire{ get; private set; }

    public void InitBehaviourHandler(IBehaviourHandler behaviourHandler) => this.behaviourHandler = behaviourHandler; 
    public void InitIdleBeh(IIdleBehaviour idle) => this.idle = idle;
    public void InitMoveBeh(IMoveBehaviour move) => this.move = move;
    public void InitWalkBeh(IWalkBehaviour walk) => this.walk = walk;
    public void InitCrouchBeh(ICrouchBehaviour crouch) => this.crouch = crouch;
    public void InitRunBeh(IRunBehaviour run) => this.run = run;
    public void InitSprintBeh(ISprintBehaviour sprint) => this.sprint = sprint;
    public void InitRotateBeh(IRotateBehaviour rotate) => this.rotate = rotate;
    public void InitLeanBeh(ILeanBehaviour lean) => this.lean = lean;
    public void InitJumpBeh(IJumpBehaviour jump) => this.jump = jump;
    public void InitParkourBeh(IParkourBehaviour parkour) => this.parkour= parkour;
    public void InitPickUpBeh(IPickUpItemBehaviour pickUp) => this.pickUp = pickUp;
    public void InitReadyBeh(IReadyForBattleBehaviour ready) => this.ready = ready;
    public void InitAimBeh(IAimWeaponBehaviour aim) => this.aim = aim;
    public void InitEquipWeaponBeh(IEquipWeaponBehaviour equip) => this.equip = equip;
    public void InitReloadWeaponBeh(IReloadWeaponBehaviour reaload) => this.reaload = reaload;
    public void InitFireWeaponBeh(IFireWeaponBehaviour fire) => this.fire = fire;
   
}
