using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    //Attributes
    //Serialized Fields
    [SerializeField] public PlayerController player;
    [SerializeField] public GameManager gameMnger;

    //Not Serialized Fields


    //Methods


    // Start is called before the first frame update
    void Start()
    {
        //Check Player and instantiate
        if(PlayerController.instance == null) {
            PlayerController playerClone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = playerClone;
        }

        if(GameManager.instance == null) {
            //GameManager gameMngerClone = Instantiate(gameMnger).GetComponent<GameManager>();
            //GameManager.instance = gameMngerClone;
            Instantiate(gameMnger);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
