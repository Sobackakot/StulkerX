
using NPC.Main;
using System;
using Zenject;
using Character.Camera;

public class MainHandler : ITickable, ILateTickable, IFixedTickable , IInitializable, IDisposable
{   
    public MainHandler(
        CameraController camera, 
        CharacterMoveMain character, 
        CharacterAnimatorMain animator,
        NPC_Main mainNPC)
    {   
        this.character = character;
        this.animator = animator;
        this.camera = camera; 
        this.mainNPC = mainNPC;
    }
    private CameraController camera;
    private CharacterMoveMain character;
    private CharacterAnimatorMain animator; 
    private NPC_Main mainNPC;
   
    public void Initialize()
    {
        character?.Initialize();
    }

    public void Dispose()
    {
        character.Dispose();
    }
    public void Tick()
    { 
        character?.Tick();
        animator?.Tick();  
        mainNPC?.Tick();
        camera?.Tick(); 
    }
    public void LateTick()
    { 
        character?.LateTick();
        animator?.LateTick();  
        mainNPC?.LateTick();
        camera?.LateTick(); 
    }
    public void FixedTick()
    { 
        character?.FixedTick();
        animator?.FixedTick();  
        mainNPC?.FixedTick();
        camera?.FixedTick(); 
    } 
}
