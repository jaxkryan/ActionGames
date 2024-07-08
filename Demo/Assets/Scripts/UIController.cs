using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void StartBtn_Click()
    {
        SceneManager.LoadScene("Room_Start");
    }

    public void RestartBtn_Click()
    {
        SceneManager.LoadScene("Room_Start");
    }

    public void MainMenuBtn_Click()
    {
        SceneManager.LoadScene("MainMenu_Screen");
    }

    public void OptionsBtn_Click()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.OpenSettings();
        }
    }

    public void ExitBtn_Click()
    {
        Application.Quit();
    }

    public void CloseSettingsBtn_Click()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.CloseSettings();
        }
    }


}