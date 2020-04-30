using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    //Atributes
    [SerializeField] public string areaEntranceName;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(areaEntranceName);
        if (areaEntranceName == PlayerController.instance.areaTransitionName) {
            PlayerController.instance.transform.position = transform.position;
        } else if(PlayerController.instance.areaTransitionName != null){
            PlayerController.instance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
