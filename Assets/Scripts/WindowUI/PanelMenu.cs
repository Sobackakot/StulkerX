using Character.MainCamera;
using UnityEngine;
using UnityEngine.SceneManagement;
using Window.UI;

namespace Menu 
{
    //This Game scene = 1;
    public class PanelMenu : MonoBehaviour
    {
        // later need set inactive inventory for active Menu
        private GameObject charCam;
        private GameObject windowUI;
        private GameObject menuUI; // instance for Camera_menu and Canvas_Menu
        public bool isActiveMenu { get; private set; } = true;

        private void OnEnable()
        {
            charCam = FindObjectOfType<FreeCameraCharacter>()?.gameObject; 
            menuUI = GetComponentInChildren<MenuGameObjectUI>()?.gameObject;
            windowUI = WindowUI.instanceUI?.GetComponent<WindowUI>().gameObject;
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
            menuUI?.SetActive(isActiveMenu);
            charCam?.SetActive(!isActiveMenu);
            windowUI?.SetActive(!isActiveMenu); 
        }
       
        private void UpdateCursorLockState()
        {
            Cursor.lockState = isActiveMenu ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = isActiveMenu ? 0 : 1;
        }
    }

}

