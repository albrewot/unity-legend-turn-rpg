using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour {
    [SerializeField] public static Shop instance;
    [SerializeField] public GameObject shopMenu;
    [SerializeField] public GameObject buyWindow;
    [SerializeField] public GameObject sellWindow;
    [SerializeField] public GameObject[] windows;
    [SerializeField] public TextMeshProUGUI goldText;

    //Methods
    public void OpenShop() {
        shopMenu.SetActive(true);
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
        ToggleWindow(0);
        PlayerController.instance.currentState = PlayerState.menu;
    }

    public void CloseShop() {
        shopMenu.SetActive(false);
        ToggleWindow(0);
        PlayerController.instance.currentState = PlayerState.walk;
    }

    public void ToggleWindow(int windowIndex) {
        for (int i = 0; i < windows.Length; i++) {
            if (i == windowIndex) {
                windows[i].SetActive(true);
            } else {
                windows[i].SetActive(false);
            }
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
