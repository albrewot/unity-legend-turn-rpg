using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCBehavior : MonoBehaviour
{
    //Attributes
    [SerializeField] public string[] dialogueLines;
    [SerializeField] public GameObject interactBox;
    [SerializeField] public TextMeshProUGUI interactText;
    [SerializeField] public string npcName;
    [SerializeField] public string interactDialog;
    [SerializeField] public bool playerInRange;
    [SerializeField] public bool isPerson = true;

    [Header("Quest Behaviour")]
    [SerializeField] public bool shouldActivateQuest;
    [SerializeField] public string questToMark;
    [SerializeField] public bool markComplete;
    

    //Methods
    public void showDialogueBox() {
        if(playerInRange && Input.GetKeyDown(KeyCode.F)) {
            interactBox.SetActive(false);
            activateQuest();
            DialogueManager.instance.OpenDialogBox();
        }
    }

    public void activateQuest() {
        if (shouldActivateQuest) {
            Debug.Log("Should activate Quest");
            DialogueManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        showDialogueBox();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerInRange = true;
            interactText.text = interactDialog;
            DialogueManager.instance.SetDialogueLines(dialogueLines, npcName, isPerson);
            interactBox.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerInRange = false;
            DialogueManager.instance.CloseDialogBox();
            interactBox.SetActive(false);
        }
    }
}
