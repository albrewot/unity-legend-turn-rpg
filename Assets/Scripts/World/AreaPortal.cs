using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPortal : MonoBehaviour
{
    [SerializeField] public string sceneToLoad;
    [SerializeField] public AreaEntrance entrance;
    [SerializeField] public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        entrance.areaEntranceName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            //StartCoroutine(LoadScene());
            SceneLoader.instance.LoadScene(sceneToLoad);
            PlayerController.instance.areaTransitionName = areaTransitionName;
            PlayerController.instance.SetPlayerIdleAnimation();
            PlayerController.instance.currentState = PlayerState.transfer;
        }
    }
}
