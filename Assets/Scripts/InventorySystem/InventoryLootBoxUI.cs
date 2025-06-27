
using UnityEngine;
using Zenject;

namespace Inventory_
{
    public class InventoryLootBoxUI : MonoBehaviour
    { 
        private CharacterInputEventHandler inputEvent;

        [Inject]
        private void Construct(CharacterInputEventHandler state)
        {
            this.inputEvent = state;
        } 
        private void Start()
        {
            gameObject.SetActive(false); 
        }
        private void OnEnable()
        {
            inputEvent.OnActiveInventory += InputCharacter_OnExitInventoryLootBox;  
        }
        private void OnDisable()
        {
            inputEvent.OnActiveInventory -= InputCharacter_OnExitInventoryLootBox; 
        }  
        public void InputCharacter_OnExitInventoryLootBox(bool isActive)
        {
            gameObject.SetActive(false);
        }
    }
}

