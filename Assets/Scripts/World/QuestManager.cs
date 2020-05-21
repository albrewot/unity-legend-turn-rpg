using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance; 

    [SerializeField] public string[] questMarkerNames;
    [SerializeField] public bool[] questMarkerComplete;

    //Methods
    public int findQuest(string questToFind) {
        for(int i = 0; i < questMarkerNames.Length; i++) {
            if(questMarkerNames[i] == questToFind) {
                return i;
            }
        }

        Debug.LogError($"Quest \"{questToFind}\" does not exist");
        return 0;
    }

    public bool checkIfComplete(string questToCheck) {
        int questIndex = findQuest(questToCheck);
        
        if(questIndex != 0) {
            return questMarkerComplete[questIndex];
        }

        return false;
    }

    public void MarkQuestComplete(string questToMark) {
        int questIndex = findQuest(questToMark);
        if(questMarkerComplete[questIndex] == false) {
            questMarkerComplete[questIndex] = true;
            UpdateLocalQuestObjects();
        } else {
            Debug.LogError($"Quest \"{questToMark}\" is already completed");
        }
    }

    public void MarkQuestIncomplete(string questToMark) {
        int questIndex = findQuest(questToMark);
        if (questMarkerComplete[questIndex] == true) {
            questMarkerComplete[questIndex] = false;
            UpdateLocalQuestObjects();
        } else {
            Debug.LogError($"Quest \"{questToMark}\" wasn't completed to begin with");
        }
    }

    public void UpdateLocalQuestObjects() {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();
        Debug.Log($"Objects found: {questObjects.Length}");
        if(questObjects.Length > 0) {
            for(int i = 0; i < questObjects.Length; i++) {
                questObjects[i].CheckCompletion();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkerComplete = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log(checkIfComplete("Catch a donkey"));
            MarkQuestComplete("Give me gold");
            MarkQuestIncomplete("Fight rats");
        }
    }
}
