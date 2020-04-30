using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    [SerializeField] public static DialogueManager instance;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public GameObject speakerBox;
    [SerializeField] public TextMeshProUGUI dialogueText;
    [SerializeField] public GameObject interactBox;
    [SerializeField] public TextMeshProUGUI speakerText;
    [SerializeField] public string[] dialogueLines;
    [SerializeField] public string speakerName;
    [SerializeField] public string npcName;
    [SerializeField] public int currentLine;
    [SerializeField] public float typingSpeed;

    //Methods
    public void OpenDialogBox() {
        ShowDialogLine();
    }

    public void NextLine() {
        currentLine++;
        if (currentLine >= dialogueLines.Length) {
            CloseDialogBox();
        }
        else {
            TypeLine();
        }
    }

    public void ShowDialogLine() {
        if (!dialogueBox.activeInHierarchy && dialogueLines.Length > 0) {
            currentLine = 0;
            dialogueBox.SetActive(true);
            TypeLine();
        }
        else if (dialogueLines.Length <= 0) {
            CloseDialogBox();
        }
        else {
            NextLine();
        }
    }

    public void TypeLine() {
        parseDialogLines();
        StopAllCoroutines();
        StartCoroutine(typeLinesCo());
    }

    public void CloseDialogBox() {
        dialogueBox.SetActive(false);
        interactBox.SetActive(true);
    }

    public void SetDialogueLines(string[] lines, string receivedNpcName, bool isPerson) {
        Debug.Log(isPerson);
        speakerBox.SetActive(isPerson);
        npcName = receivedNpcName;
        dialogueLines = (string[]) lines.Clone();
    }

    public void parseDialogLines() {
        
        if (dialogueLines[currentLine].StartsWith("Player:")) {
            Debug.Log("Player");
            speakerName = "Player";
            speakerText.text = speakerName;
            dialogueLines[currentLine] = dialogueLines[currentLine].Replace("Player:","");
        }
        else {
            Debug.Log("NPC");
            speakerName = npcName;
            speakerText.text = speakerName;
            dialogueLines[currentLine] = dialogueLines[currentLine].Replace("NPC:","");
        }
    }

    //Coroutines
    IEnumerator typeLinesCo() {
        dialogueText.text = "";
        foreach (char letter in dialogueLines[currentLine].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {

    }
}
