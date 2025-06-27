namespace State.Character
{
    public class CharacterStateBootstrap  
    {
        public CharacterStateBootstrap()
        { 
            Camera = new CharacterStateCamera(this);
           
        }   
        public CharacterStateCamera Camera { get; private set; } 
    }
     
}

