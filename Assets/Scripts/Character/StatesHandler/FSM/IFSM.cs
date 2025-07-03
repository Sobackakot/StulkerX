namespace State.CoreFSM
{
    public interface IFSM
    { 
        void TransitionFSM();
        void UpdateFSM();
        void LateUpdateFSM();
        void FixedUpdateFSM();
    }
}