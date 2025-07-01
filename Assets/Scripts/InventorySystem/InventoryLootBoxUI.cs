
using UnityEngine;
using Character.InputEvents;
using Zenject;

namespace Inventory
{
    public class InventoryLootBoxUI : MonoBehaviour
    { 
        private IInputEvents inputEvent;

        [Inject]
        private void Construct(IInputEvents state)
        {
            this.inputEvent = state;
        } 
        private void Start()
        {
            gameObject.SetActive(false); 
        }
        private void OnEnable()
        {
            if(inputEvent!= null)
                inputEvent.OnActiveInventory += InputCharacter_OnExitInventoryLootBox;  
        }
        private void OnDisable()
        {
            if (inputEvent != null)
                inputEvent.OnActiveInventory -= InputCharacter_OnExitInventoryLootBox; 
        }  
        public void InputCharacter_OnExitInventoryLootBox(bool isActive)
        {
            gameObject.SetActive(false);
        }
    }
}

