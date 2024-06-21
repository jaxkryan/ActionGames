using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Array of scene names to choose from
    public string[] sceneNames;
    private string previousScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save player state
            SavePlayerState();

            // Load a random scene
            LoadRandomScene();
        }
    }

    private void SavePlayerState()
    {
        // TODO: Implement saving player state
        // This could involve saving health, position, inventory, etc.
        Debug.Log("Saving player state...");
    }

    private void LoadRandomScene()
    {
        if (sceneNames.Length > 1) // Ensure there are at least two scenes to choose from
        {
            string randomScene;
            do
            {
                // Choose a random scene from the array
                randomScene = sceneNames[Random.Range(0, sceneNames.Length)];
            } while (randomScene == previousScene);

            // Set the previous scene to the current one before loading the new scene
            previousScene = randomScene;

            // Load the chosen scene
            SceneManager.LoadScene(randomScene);
        }
        else if (sceneNames.Length == 1)
        {
            // If there is only one scene, just load it
            previousScene = sceneNames[0];
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogError("No scenes assigned to the RandomSceneLoader!");
        }
    }

    public void StartBtn_Click()
    {
        SceneManager.LoadScene("Room_Start");
    }
    public void RestartBtn_Click()
    {
        SceneManager.LoadScene("Room_Start");
    }public void MainMenuBtn_Click()
    {
        SceneManager.LoadScene("MainMenu_Screen");
    }
}
