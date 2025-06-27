namespace State.Character
{
    public abstract class CharacterStateBase  
    {
        public CharacterStateBase(CharacterStateBootstrap stateGame)
        {
            this.stateGame = stateGame;
            EnterState();
        }
        ~CharacterStateBase()
        {
            ExitState();
        }

        protected readonly CharacterStateBootstrap stateGame;
        public virtual void EnterState()
        {

        }

        public virtual void ExitState()
        {

        }

        public virtual void UpdateState()
        {

        }
    }
}

