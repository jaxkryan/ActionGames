using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
}
