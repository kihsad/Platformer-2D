using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class UIComponent : MonoBehaviour
    {
       
        [SerializeField]
        private GameObject _settingsPanel;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "MenuScreen")
            {
                CloseSettings(); 
            }
        }
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowSettings()
        {
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            _settingsPanel.SetActive(false);
        }
        public void StartNewGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
        }

    }
}
