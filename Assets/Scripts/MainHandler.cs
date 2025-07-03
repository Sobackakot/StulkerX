
using Character.MainCamera.BootStrap;
using NPC.Main;
using StateData.Character;
using System;
using Zenject;

public class MainHandler : ITickable, ILateTickable, IFixedTickable , IInitializable, IDisposable
{   
    public MainHandler(
        CharacterStateContext context,
        MainCameraEntryPoint camera, 
        CharacterMoveMain character, 
        CharacterAnimatorMain animator,
        NPC_Main mainNPC)
    {
        this.context = context;
        this.character = character;
        this.animator = animator;
        this.camera = camera; 
        this.mainNPC = mainNPC;
    }
    private CharacterStateContext context;
    private MainCameraEntryPoint camera;
    private CharacterMoveMain character;
    private CharacterAnimatorMain animator; 
    private NPC_Main mainNPC;
   
    public void Initialize()
    {
        character?.Initialize();
    }

    public void Dispose()
    {
        character?.Dispose();
    }
    public void Tick()
    {
        if (context.isActiveInventory) return;
        character?.Tick();
        animator?.Tick();  
        mainNPC?.Tick();
        camera?.Tick(); 
    }
    public void LateTick()
    {
        if (context.isActiveInventory) return;
        character?.LateTick();
        animator?.LateTick();  
        mainNPC?.LateTick();
        camera?.LateTick(); 
    }
    public void FixedTick()
    {
        if (context.isActiveInventory) return;
        character?.FixedTick();
        animator?.FixedTick();  
        mainNPC?.FixedTick();
        camera?.FixedTick(); 
    } 
}
