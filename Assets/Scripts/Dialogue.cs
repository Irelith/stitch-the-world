using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour{
    
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private bool startDialogueAutomatically = false;
    [SerializeField, TextArea(3, 10)] private string[] dialogueLines;

    private float typeSpeed = 0.05f;

    private bool isPlayerInRange;
    private bool isDialogueActive;
    private int currentLineIndex;

    void Start(){
        if (startDialogueAutomatically){
            isPlayerInRange = true;
            StrartDialogue();
        }
    }

    // Update is called once per frame
    void Update(){
        if (PauseMenu.GameIsPaused) return; // Skip if the game is paused
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)){
            if (!isDialogueActive){
                StrartDialogue();
            }else if(dialogueText.text == dialogueLines[currentLineIndex]){
                nextLine();
            }else{
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLineIndex];
            }
        }
    }

    void StrartDialogue(){
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        if (dialogueMark != null){
            dialogueMark.SetActive(false);
        }
        currentLineIndex = 0;
        Time.timeScale = 0f; // Pause the game
        StartCoroutine(DisplayDialogue());
    }

    private void nextLine(){
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length){
            StartCoroutine(DisplayDialogue());
        } else {
            EndDialogue();
        }
    }

    private IEnumerator DisplayDialogue(){
        dialogueText.text = string.Empty;
        foreach (char letter in dialogueLines[currentLineIndex]){
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
    }

    private void EndDialogue(){
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        if (dialogueMark != null){
            dialogueMark.SetActive(true);
        }
        Time.timeScale = 1f; // Start the game
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            isPlayerInRange = true;
            if (dialogueMark != null){
                dialogueMark.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            isPlayerInRange = false;
            if (dialogueMark != null){
                dialogueMark.SetActive(false);
            }
        }
    }

    public void TriggerDialogueExternally(){
        if (!isDialogueActive){
            isPlayerInRange = true;
            StrartDialogue();
        }
    }
}
