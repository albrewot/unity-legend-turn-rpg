using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Attributes
    [SerializeField] public static GameManager instance;
    [SerializeField] public CharacterStats[] playerStats;

    //Methods
    public void InitializeGamemanager() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        InitializeGamemanager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
