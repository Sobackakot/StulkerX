namespace Behaviour.Character
{
    public interface IReadyForBattleBehaviour : IUnitBehaviour
    {
        void ReadyForBattle(bool isReady);
    }
}