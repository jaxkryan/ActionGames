using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public List<string[]> dialogueSets;  // List of dialogue sets
    private string[] currentDialogue;
    private int index;

    public GameObject nextButton;
    public float wordSpeed;
    public bool playerIsClose;

    // EnemySpawnPoint reference
    public GameObject[] enemySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSets = new List<string[]>();  // Initialize the list
        // Example of adding dialogue sets
        dialogueSets.Add(new string[] { "Hello there!", "How are you?", "Nice weather today." });
        dialogueSets.Add(new string[] { "Greetings!", "What brings you here?", "Take care!" });
        // Add more dialogue sets as needed

        // Find EnemySpawnPoint by tag
        enemySpawnPoint = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        if (enemySpawnPoint == null)
        {
            Debug.LogError("EnemySpawnPoint not found! Make sure it's tagged correctly.");
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in currentDialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                EndDialogue();
            }
            else
            {
                StartDialogue();
            }
        }
        if (dialogueText.text == currentDialogue[index])
        {
            nextButton.SetActive(true);
        }
    }

    public void NextLine()
    {
        nextButton.SetActive(false);
        if (index < currentDialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        // Disable EnemySpawnPoint
        if (enemySpawnPoint != null)
        {
            foreach (GameObject e in enemySpawnPoint)
            {
                e.SetActive(false);
            }
        }

        // Select random dialogue set
        SelectRandomDialogue();
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
    }

    private void EndDialogue()
    {
        // Enable EnemySpawnPoint
        if (enemySpawnPoint != null)
        {
            foreach (GameObject e in enemySpawnPoint)
            {
                e.SetActive(true);
            }
           
        }

        // Reset dialogue UI
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void SelectRandomDialogue()
    {
        if (dialogueSets.Count > 0)
        {
            int randomIndex = Random.Range(0, dialogueSets.Count);
            currentDialogue = dialogueSets[randomIndex];
            index = 0;  // Reset index for new dialogue
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            EndDialogue();
        }
    }
}
