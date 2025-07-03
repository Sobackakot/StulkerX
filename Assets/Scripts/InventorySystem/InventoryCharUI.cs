using Character.InputEvents;
using UnityEngine;
using Zenject;


namespace Inventory
{
    public class InventoryCharUI : MonoBehaviour
    {
        private IInputEvents inputEvent;

        [Inject]
        private void Construct(IInputEvents inputEvent)
        {
            this.inputEvent = inputEvent;
        }
        private void Start()
        {
            gameObject.SetActive(false); 
        }
        private void OnEnable()
        {
            if (inputEvent != null)
            {
                inputEvent.OnActiveInventory += Input_OnActivateInventory;
                inputEvent.OnExitInventory += Input_OnExitInventory;
            } 
        }
        private void OnDestroy()// this class inactive start game - can't be in onDisable
        {
            if (inputEvent != null)
            {
                inputEvent.OnActiveInventory -= Input_OnActivateInventory;
                inputEvent.OnExitInventory -= Input_OnExitInventory;
            }
        }
        private void Input_OnActivateInventory(bool isActive)
        {
            gameObject.SetActive(isActive); 
        }
        private void Input_OnExitInventory()
        {
            gameObject.SetActive(false);
        }
        public void SetActiveInventory(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}

