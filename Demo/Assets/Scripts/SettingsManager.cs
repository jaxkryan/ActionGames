using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public Canvas settingsCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.gameObject.SetActive(!settingsCanvas.gameObject.activeSelf);
        }
    }

    public void OpenSettings()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.gameObject.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.gameObject.SetActive(false);
        }
    }
}