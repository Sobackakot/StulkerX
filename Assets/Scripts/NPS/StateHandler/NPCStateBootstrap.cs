using State.CoreFSM;

public class NPCStateBootstrap  
{
    public NPCStateBootstrap()
    {
        Move = new NPCStateMove(this);
        MoveAnim = new NPCStateMoveAnim(this);
        Weapon = new NPCStateWeapon(this);
    } 
    public NPCStateMove Move { get; private set; }
    public NPCStateMoveAnim MoveAnim { get; private set; }
    public NPCStateWeapon Weapon { get; private set; }
}
