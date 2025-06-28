
using Inventory_;
using State.Character;
using NPC;
using NPC.Target;
using UnityEngine;
using Zenject;
using NPC.Main;
using StateData.Character;
using Character.InputEvents;


[CreateAssetMenu(fileName = "Installer(State)", menuName = "Installers/State")]
public class CharacterInstaller : ScriptableObjectInstaller 
{
    private const string Inventory_ID = "inventory";
    private const string InventoryBox_ID = "inventoryBox";
    private const string InventoryEquip_ID = "inventoryEquip";

    private const string InventoryUI_ID = "inventoryUI";
    private const string InventoryBoxUI_ID = "inventoryBoxUI";
    private const string EquipmentUI_ID = "equipmentUI";

    private const string CameraTird_ID = "cameraTird";
    private const string CameraFerst_ID = "firstCam";
     

    public override void InstallBindings()
    {
        FSM(); 
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
    private void FSM()
    {
        Container.Bind<CharacterStateBootstrap>().FromNew().AsSingle().NonLazy(); 
    }
     
    private void BindCamera()
    {
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputCamera>().AsSingle().NonLazy();  
        Container.Bind<ICameraCharacter>().WithId(CameraTird_ID).To<FreeCameraCharacter>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<ICameraCharacter>().WithId(CameraFerst_ID).To<FirstCameraCharacter>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<RaycastCamera>().FromComponentInHierarchy(this).AsSingle();
    }

    private void BindCharacter()
    {
        Container.BindInterfacesAndSelfTo<InputCharacter>().FromNew().AsSingle().NonLazy();
        Container.Bind<CharacterMoveMain>().FromNew().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<CharacterInputEventHandler>().FromNew().AsSingle().NonLazy(); 
          
        Container.Bind<CharacterStateContext>().FromNew().AsSingle().NonLazy();   
        //MonoBehaviour  
        Container.Bind<CharacterInspector>().FromComponentInHierarchy(this).AsSingle();
        
        Container.Bind<CharacterIK>().FromComponentInHierarchy(this).AsSingle();    
    }
    private void BindInventory()
    {
        // Bind InventoryUI with an identifier
        Container.Bind<IInventoryUI>().WithId(InventoryUI_ID).To<InventoryUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<IInventoryUI>().WithId(InventoryBoxUI_ID).To<InventoryBoxUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<IInventoryUI>().WithId(EquipmentUI_ID).To<EquipmentUI>().FromComponentInHierarchy(this).AsSingle();

        Container.Bind<InventoryCharUI>().FromComponentInHierarchy(this).AsSingle();
        Container.Bind<InventoryLootBoxUI>().FromComponentInHierarchy(this).AsTransient();


        Container.Bind<IInventoryController>().WithId(Inventory_ID).To<InventoryController>().AsSingle().NonLazy(); 

        Container.Bind<IInventoryController>().WithId(InventoryEquip_ID).To<EquipmentController>().AsSingle().NonLazy(); 

        Container.Bind<IInventoryController>().WithId(InventoryBox_ID).To<InventoryBoxController>().AsSingle().NonLazy(); 
    }
    private void BindAnimatorChar()
    {
        Container.Bind<CharacterAnimatorMain>().FromNew().AsSingle().NonLazy();
        Container.Bind<CharacterAnimatorInspector>().FromComponentInHierarchy(this).AsSingle(); 
    }
}
