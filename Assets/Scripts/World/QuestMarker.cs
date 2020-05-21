using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    [SerializeField] public string questToMark;
    [SerializeField] public bool markComplete;
    [SerializeField] public bool markOnEnter;
    [SerializeField] public bool deactivateOnMarking;

    private bool canMark;

    //Methods
    public void MarkQuest() {
        if(markComplete) {
            canMark = false;
            QuestManager.instance.MarkQuestComplete(questToMark);
        } else {
            QuestManager.instance.MarkQuestIncomplete(questToMark);
        }

        gameObject.SetActive(!deactivateOnMarking);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMark && Input.GetKeyDown(KeyCode.F)) {
            canMark = false;
            MarkQuest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (markOnEnter) {
                MarkQuest();
            } else {
                canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            canMark = false;
        }
    }
}
