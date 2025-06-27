using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GameMainMenu : MonoBehaviour
    {

        public GameObject panelSettingsGame;
        public void StartGameButton()
        {
            //start new game
            SceneManager.LoadScene(1);
        }
        public void LoadLastSaveButton()
        {
            //load save;
            SceneManager.LoadScene(1);
        }
        public void OppenSettingsGameButton()
        {
            // oppen panel settings
            panelSettingsGame.SetActive(true);
        }
        public void CloseSettingsGameButton()
        {
            // oppen panel settings
            panelSettingsGame.SetActive(false);
        }
        public void ExitGameButton()
        {
            // exit game
            Application.Quit();
        }
    }
}

