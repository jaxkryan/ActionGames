using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[][] dialogueSets;  // 2D array for multiple dialogue sets
    private int index;
    private string[] currentDialogue;  // Current dialogue array

    public GameObject nextButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        // Example of initializing dialogue sets
        dialogueSets = new string[][] {
            new string[] { "Hello there!", "How are you?", "Goodbye!" },
            new string[] { "Hi!", "Nice to see you.", "See you later!" },
            new string[] { "Greetings!", "What's up?", "Farewell!" }
        };
    }

    IEnumerator Typing()
    {
        if (dialogueText != null)
        {
            foreach (char letter in currentDialogue[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !AreEnemiesPresent())
        {
            if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
            {
                zeroText();
                ResumeGame();
            }
            else
            {
                SelectRandomDialogueSet();
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(true);
                }
                PauseGame();
                StartCoroutine(Typing());
            }
        }
        if (dialogueText != null && dialogueText.text == currentDialogue[index])
        {
            if (nextButton != null)
            {
                nextButton.SetActive(true);
            }
        }
    }

    public void NextLine()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }
        if (index < currentDialogue.Length - 1)
        {
            index++;
            if (dialogueText != null)
            {
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
        }
        else
        {
            zeroText();
            ResumeGame();
        }
    }

    public void zeroText()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
        index = 0;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
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
            zeroText();
            ResumeGame();
        }
    }

    private void SelectRandomDialogueSet()
    {
        int randomIndex = Random.Range(0, dialogueSets.Length);
        currentDialogue = dialogueSets[randomIndex];
        index = 0;  // Reset index for new dialogue
    }

    private void PauseGame()
    {
        // Disable player movement and enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = false;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Knight>().enabled = false;
        }
    }

    private void ResumeGame()
    {
        // Enable player movement and enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Knight>().enabled = true;
        }
    }

    private bool AreEnemiesPresent()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }
}
