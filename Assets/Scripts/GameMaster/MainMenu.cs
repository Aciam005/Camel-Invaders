using UnityEngine;
using UnityEngine.SceneManagement;

namespace CamelInvaders.GameMaster.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
