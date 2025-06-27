using System;
using UnityEngine;
using Zenject;
using State.Character;


namespace Inventory_
{
    public class InventoryCharUI : MonoBehaviour
    {
        private CharacterInputEventHandler inputEvent;

        [Inject]
        private void Construct(CharacterInputEventHandler inputEvent)
        {
            this.inputEvent = inputEvent;
        }
        private void Start()
        {
            gameObject.SetActive(false); 
        }
        private void OnEnable()
        {
            inputEvent.OnActiveInventory += Input_OnActivateInventory;
            inputEvent.OnExitInventory += Input_OnExitInventory; 
        }
        private void OnDestroy()// this class inactive start game - can't be in onDisable
        {
            inputEvent.OnActiveInventory -= Input_OnActivateInventory;
            inputEvent.OnExitInventory -= Input_OnExitInventory; 
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

