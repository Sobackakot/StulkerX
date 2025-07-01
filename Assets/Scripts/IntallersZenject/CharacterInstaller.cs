
using Character.Camera;
using Character.InputEvents;
using Inventory;
using Inventory.Handler;
using Inventory.UI;
using NPC;
using NPC.Main;
using NPC.Target;
using StateData.Character;
using UnityEngine;
using Zenject;
using MainCamera.Raycast;


[CreateAssetMenu(fileName = "Installer(State)", menuName = "Installers/State")]
public class CharacterInstaller : ScriptableObjectInstaller 
{  
    public override void InstallBindings()
    { 
        BindCharacter();
        BindCamera();
        BindInventory();
        BindAnimatorChar();
        BindTargetSystem();
        BindWeaponEffects();
        Container.BindInterfacesAndSelfTo<MainHandler>().AsSingle().NonLazy(); 
        Container.Bind<WindowUI>().FromComponentInHierarchy(this).AsSingle(); 
    }
    private void BindWeaponEffects()
    {
        Container.BindInterfacesAndSelfTo<FireEffectsMain>().AsSingle().NonLazy();

        Container.Bind<Audios>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<WeaponEffects>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<Lights>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<FireMeshRenderer>().FromComponentInHierarchy(this).AsSingle();
    }
    private void BindTargetSystem()
    {
        Container.BindInterfacesAndSelfTo<RegistryTargets>().FromNew().AsSingle().NonLazy(); 
        Container.BindInterfacesAndSelfTo<RegistryNPC>().FromNew().AsSingle().NonLazy(); 
        Container.BindInterfacesAndSelfTo<NPC_Main>().FromNew().AsSingle().NonLazy(); 
    } 
    private void BindCamera()
    {
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputCamera>().AsSingle().NonLazy();  
        Container.Bind<IFreeCamera>().To<FreeCameraCharacter>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<IFirstCamera>().To<FirstCameraCharacter>().FromComponentInHierarchy(this).AsSingle();

        Container.BindInterfacesAndSelfTo<RaycastPointCamera>().FromComponentInHierarchy(this).AsCached();
    }

    private void BindCharacter()
    {
        Container.BindInterfacesAndSelfTo<InputCharacter>().FromNew().AsSingle().NonLazy();
        Container.Bind<CharacterMoveMain>().FromNew().AsSingle().NonLazy();
        
        Container.Bind<IInputEvents>().To<CharacterInputEventHandler>().FromNew().AsSingle().NonLazy();

        Container.Bind<CharacterStateContext>().FromNew().AsSingle();
        //MonoBehaviour  
        Container.Bind<CharacterInspector>().FromComponentInHierarchy(this).AsSingle();
        
        Container.Bind<CharacterIK>().FromComponentInHierarchy(this).AsSingle();    
    }
    private void BindInventory()
    {
        // Bind InventoryUI with an identifier
        Container.Bind<IInventoryUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<IInvetnoryLootBoxUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<IInventoryEquipmentUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<InventoryCharUI>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<InventoryLootBoxUI>().FromComponentInHierarchy(this).AsTransient();


        Container.Bind<IInventoryHandler>().To<InventoryController>().AsSingle().NonLazy(); 

        Container.Bind<IInventoryEquipmentHandler>().To<EquipmentController>().AsSingle().NonLazy(); 

        Container.Bind<IInventoryLootBoxHandler>().To<InventoryBoxController>().AsSingle().NonLazy(); 
    }
    private void BindAnimatorChar()
    {
        Container.Bind<CharacterAnimatorMain>().FromNew().AsSingle().NonLazy();
        Container.Bind<CharacterAnimatorInspector>().FromComponentInHierarchy(this).AsSingle(); 
    }
}
