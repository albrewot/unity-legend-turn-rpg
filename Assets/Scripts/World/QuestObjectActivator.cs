using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    [SerializeField] public GameObject objectToActivate;
    [SerializeField] public string questToCheck;
    [SerializeField] public bool activeifComplete;

    private bool initialCheckDone;

    //Methods
    public void CheckCompletion() {
        if (QuestManager.instance.checkIfComplete(questToCheck)) {
            objectToActivate.SetActive(activeifComplete);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone) {
            initialCheckDone = true;
            CheckCompletion();
        }
    }
}
