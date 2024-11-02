using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MUI
{
    public class PauseSystemUIManager : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject PauseSettings;
        [SerializeField] private GameObject RestartSettingsChoice;
        [SerializeField] private GameObject SaveSettingsChoice;
        [SerializeField] private GameObject OptionsSettingsChoice;
        [SerializeField] private GameObject QuitSettingsChoice;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }
        }

        public void Resume()
        {
            if (PauseSettings.active != true)
            {
                PauseSettings.SetActive(true);
            }
            else
            {
                PauseSettings.SetActive(false);
            }
        }

        public void Restart()
        {
            if (RestartSettingsChoice.active != true)
            {
                RestartSettingsChoice.SetActive(true);
            }
            else
            {
                RestartSettingsChoice.SetActive(false);
            }
        }

        public void RestartYes(string _Scene)
        {
            SceneManager.LoadScene(_Scene);
        }

        public void Save()
        {
            if (SaveSettingsChoice.active != true)
            {
                SaveSettingsChoice.SetActive(true);
            }
            else
            {
                SaveSettingsChoice.SetActive(false);
            }
        }

        public void Options()
        {
            if (OptionsSettingsChoice.active != true)
            {
                OptionsSettingsChoice.SetActive(true);
            }
            else
            {
                OptionsSettingsChoice.SetActive(false);
            }
        }

        public void Quit() 
        {
            if (QuitSettingsChoice.active != true)
            {
                QuitSettingsChoice.SetActive(true);
            }
            else
            {
                QuitSettingsChoice.SetActive(false);
            }
        }

        public void Exit()
        { 
            Application.Quit();
        }

    }
}

