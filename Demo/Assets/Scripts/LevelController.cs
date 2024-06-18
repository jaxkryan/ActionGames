using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Array of scene names to choose from
    public string[] sceneNames;

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
        if (sceneNames.Length > 0)
        {
            // Choose a random scene from the array
            string randomScene = sceneNames[Random.Range(0, sceneNames.Length)];

            // Load the chosen scene
            SceneManager.LoadScene(randomScene);
        }
        else
        {
            Debug.LogError("No scenes assigned to the RandomSceneLoader!");
        }
    }
}
