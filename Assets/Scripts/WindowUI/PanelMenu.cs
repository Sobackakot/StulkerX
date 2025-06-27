
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu 
{
    //This Game scene - 1;
    public class PanelMenu : MonoBehaviour
    {
        // later need set inactive inventory for active Menu
        public GameObject personCamera;
        public GameObject personWindowUI;
        public GameObject panelMenu; // instance for Camera_menu and Canvas_Menu
        public bool isActiveMenu { get; private set; } = true;
        private void OnEnable()
        {
            SetMenuState(false);
            UpdateCursorLockState();
        }
         
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogleActiveMenu();
            } 
        }
        private void TogleActiveMenu()
        {
            SetMenuState(!isActiveMenu);
            UpdateCursorLockState();
        }
        public void ContinueGameButton()
        {
            SetMenuState(false);
            UpdateCursorLockState();
        }
        public void LoadMenuSceneButton()
        { 
            SceneManager.LoadScene(0);
        }
        private void SetMenuState(bool isActiva)
        {
            isActiveMenu = isActiva;
            panelMenu.SetActive(isActiveMenu);
            personCamera.SetActive(!isActiveMenu);
            personWindowUI.SetActive(!isActiveMenu); 
        }
       
        private void UpdateCursorLockState()
        {
            Cursor.lockState = isActiveMenu ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = isActiveMenu ? 0 : 1;
        }
    }

}

